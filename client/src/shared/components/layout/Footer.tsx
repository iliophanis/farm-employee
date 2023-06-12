import UnderlineLink from '@/shared/components/links/UnderlineLink';
import clsxm from '@/shared/lib/clsxm';
import { useRouter } from 'next/router';
import React from 'react';

const Footer = () => {
  const router = useRouter();
  console.log(router.pathname);
  const className =
    router.pathname === '/'
      ? 'absolute bottom-2 '
      : 'fixed bottom-0 left-0 z-20 w-full bg-white border-t p-2 border-gray-200 shadow  dark:bg-gray-800 dark:border-gray-600';
  return (
    <footer className={clsxm(className, 'text-gray-700')}>
      © {new Date().getFullYear()} By{' '}
      <UnderlineLink href='/about-us' openNewTab className='text-violet-700'>
        Team Farm Employee
      </UnderlineLink>
    </footer>
  );
};

export default Footer;
