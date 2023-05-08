import { AppProps } from 'next/app';
import { GoogleOAuthProvider } from '@react-oauth/google';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

import '@/shared/styles/globals.css';
// !STARTERCONF This is for demo purposes, remove @/styles/colors.css import immediately
import '@/shared/styles/colors.css';
import '@/shared/styles/main.css';
import Layout from '@/shared/components/layout/Layout';
import AuthProvider from '@/shared/contexts/AuthProvider';
import { LayoutProvider } from '@/shared/contexts/LayoutProvider';

/**
 * !STARTERCONF info
 * ? `Layout` component is called in every page using `np` snippets. If you have consistent layout across all page, you can add it here too
 */

function MyApp({ Component, pageProps }: AppProps) {
  const clientId = process.env.NEXT_PUBLIC_GOOGLE_CLIENT_ID!;
  const queryClient = new QueryClient({
    defaultOptions: {
      queries: {
        retry: false,
        refetchOnWindowFocus: false,
        // cacheTime: 0,
        suspense: true,
      },
    },
  });
  return (
    <QueryClientProvider client={queryClient}>
      <GoogleOAuthProvider clientId={clientId}>
        <AuthProvider>
          <LayoutProvider>
            <Layout>
              <Component {...pageProps} />
            </Layout>
          </LayoutProvider>
        </AuthProvider>
      </GoogleOAuthProvider>
    </QueryClientProvider>
  );
}

export default MyApp;
