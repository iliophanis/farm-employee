import { useEffect, useRef } from 'react';

const useDebounce = () => {
  const timeout: any = useRef();

  const debounce =
    (func: any, wait: number) =>
    (...args: any) => {
      clearTimeout(timeout.current);
      if (timeout) {
        timeout.current = setTimeout(() => func(...args), wait);
      }
    };

  useEffect(() => {
    return () => {
      if (!timeout.current) return;
      clearTimeout(timeout.current);
    };
  }, []);

  return { debounce };
};

export default useDebounce;
