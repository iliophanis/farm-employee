import { createContext, useContext, useState, useEffect } from 'react';
import { useMemo } from 'react';

type LayoutContextProps = {
  view: string;
};

const TABLET_BREAKPOINT = 1000;
const MOBILE_BREAKPOINT = 767;
export const LayoutContext = createContext<LayoutContextProps>({
  view: 'web',
});

export const LayoutProvider = ({ children }: any) => {
  const [layout, setLayout] = useState({
    view: 'web',
  });
  useEffect(() => {
    setLayout({
      view:
        window.innerWidth <= MOBILE_BREAKPOINT
          ? 'mobile'
          : window.innerWidth <= TABLET_BREAKPOINT
          ? 'tablet'
          : 'web',
    });
  }, []);

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth <= MOBILE_BREAKPOINT && layout.view !== 'mobile') {
        setLayout((prevLayout) => ({ ...prevLayout, view: 'mobile' }));
      }
      if (window.innerWidth > TABLET_BREAKPOINT && layout.view !== 'web') {
        setLayout((prevLayout) => ({ ...prevLayout, view: 'web' }));
      }
      if (
        window.innerWidth <= TABLET_BREAKPOINT &&
        window.innerWidth >= MOBILE_BREAKPOINT &&
        layout.view !== 'tablet'
      ) {
        setLayout((prevLayout) => ({ ...prevLayout, view: 'tablet' }));
      }
    };
    window.addEventListener('resize', handleResize);
    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, [layout]);

  const values = useMemo(() => layout, [layout]);
  return (
    <LayoutContext.Provider value={values}>{children}</LayoutContext.Provider>
  );
};

export const useLayout = () => useContext(LayoutContext);
