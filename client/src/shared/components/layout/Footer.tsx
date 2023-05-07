import UnderlineLink from '@/shared/components/links/UnderlineLink';
import React from 'react';

const Footer = () => {
  return (
    <footer className='absolute bottom-2 text-gray-700'>
      © {new Date().getFullYear()} By{' '}
      <UnderlineLink href='/team' openNewTab className='text-violet-700'>
        Team Farm Employee
      </UnderlineLink>
    </footer>
  );
};

export default Footer;
