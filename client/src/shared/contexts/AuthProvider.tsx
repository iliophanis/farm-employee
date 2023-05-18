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
  token: string;
  expires: Date;
  user: {
    displayName: string;
    userId: number;
    picture: string;
    isFarmer: boolean;
    isEmployee: boolean;
    isAdmin: boolean;
  };
}
interface IProfileSettings {
  darkMode: boolean;
}

type AuthContextProps = {
  logout: () => void;
  login: (userResponse: IUserResponse) => void;
  isAuthenticated: () => boolean;
  auth: IAuth | null;
  profileSettings: IProfileSettings | null;
};

const AuthContext = createContext<AuthContextProps>({
  logout: () => {},
  login: () => {},
  isAuthenticated: () => false,
  auth: null,
  profileSettings: null,
});

type IAuthProviderProps = {
  children: any;
};

export const AuthProvider = ({ children }: IAuthProviderProps) => {
  const [auth, setAuth] = useState<IAuth | null>(null);
  const [profileSettings, setProfileSettings] =
    useState<IProfileSettings | null>(null);
  //hint can use useMemo localstorage.getItem and check if exist

  const handleChangeAuth = useCallback(
    ({
      token,
      expiration,
      displayName,
      userId,
      role,
      picture,
    }: IUserResponse) => {
      setAuth({
        token: token,
        expires: expiration,
        user: {
          displayName: displayName,
          userId: userId,
          picture: picture,
          isFarmer: role === 'farmer',
          isAdmin: role === 'admin',
          isEmployee: role === 'employee',
        },
      });
    },
    [setAuth]
  );
  const isAuthenticatedState = (authState: any) => {
    if (authState && authState.expiration) {
      return new Date() < new Date(authState.expiration);
    }
    return false;
  };
  useEffect(() => {
    const loadUser = async () => {
      const authState = store.get('farmEmpAuth');
      const darkMode = store.get('farmEmpDarkMode');
      if (darkMode) setProfileSettings({ darkMode: darkMode });
      if (authState && isAuthenticatedState(authState)) {
        handleChangeAuth(authState);
      } else setAuth(null);
    };
    loadUser();
  }, [handleChangeAuth]);

  const login = useCallback(
    (userResponse: IUserResponse) => {
      store.set('farmEmpAuth', userResponse);
      const darkMode = store.get('farmEmpDarkMode');
      if (!darkMode) store.set('farmEmpDarkMode', 'false');
      handleChangeAuth(userResponse);
      setProfileSettings({ darkMode: darkMode ? darkMode === 'true' : false });
    },
    [handleChangeAuth]
  );

  const logout = useCallback(() => {
    store.remove('farmEmpAuth');
    setAuth(null);
  }, [setAuth]);

  const isAuthenticated = useCallback(() => {
    if (auth && auth.expires) {
      return new Date() < new Date(auth.expires);
    }
    return false;
  }, [auth]);

  const providerValue = useMemo(
    () => ({
      logout,
      login,
      auth,
      profileSettings,
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
