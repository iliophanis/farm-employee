import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from 'react';

import store from '../utils/store';
import { IUserResponse } from '@/modules/auth/models/IUser';

interface IAuth {
  darkMode: boolean;
  expires: Date;
  user: {
    displayName: string;
    userId: number;
    isFarmer: boolean;
    isEmployee: boolean;
    isAdmin: boolean;
  };
}

type AuthContextProps = {
  logout: () => void;
  login: (userResponse: IUserResponse) => void;
  isAuthenticated: () => boolean;
  auth: IAuth | null;
};

const AuthContext = createContext<AuthContextProps>({
  logout: () => {},
  login: () => {},
  isAuthenticated: () => false,
  auth: null,
});

type IAuthProviderProps = {
  children: any;
};

export const AuthProvider = ({ children }: IAuthProviderProps) => {
  const [auth, setAuth] = useState<IAuth | null>(null);
  //hint can use useMemo localstorage.getItem and check if exist

  const handleChangeAuth = useCallback(
    (
      { token, expiration, displayName, userId, roles }: IUserResponse,
      darkMode: boolean
    ) => {
      setAuth({
        darkMode: darkMode,
        expires: expiration,
        user: {
          displayName: displayName,
          userId: userId,
          isFarmer: roles.includes('farmer'),
          isAdmin: roles.includes('admin'),
          isEmployee: roles.includes('employee'),
        },
      });
    },
    [setAuth]
  );

  useEffect(() => {
    const loadUser = async () => {
      const authState = store.get('farmEmpAuth');
      const darkMode = store.get('farmEmpDarkMode');
      if (authState) handleChangeAuth(authState, darkMode);
      else setAuth(null);
    };
    loadUser();
  }, [handleChangeAuth]);

  const login = useCallback(
    (userResponse: IUserResponse) => {
      store.set('farmEmpAuth', JSON.stringify(userResponse));
      const darkMode = store.get('farmEmpDarkMode');
      if (!darkMode) store.set('farmEmpDarkMode', 'false');
      handleChangeAuth(userResponse, darkMode ? darkMode === 'true' : false);
    },
    [handleChangeAuth]
  );

  const logout = useCallback(() => {
    store.remove('farmEmpAuth');
    setAuth(null);
  }, [setAuth]);

  const isAuthenticated = useCallback(() => {
    if (auth && auth.expires) {
      return new Date() > new Date(auth.expires);
    }
    return false;
  }, [auth]);

  const providerValue = useMemo(
    () => ({
      logout,
      login,
      auth,
      isAuthenticated,
    }),
    [logout, login, auth, isAuthenticated]
  );
  return (
    <AuthContext.Provider value={providerValue}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);

export default AuthProvider;
