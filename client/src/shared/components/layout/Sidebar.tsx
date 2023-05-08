import * as React from 'react';
import { MdOutlineAgriculture } from 'react-icons/md';
import {
  HiBars3BottomLeft,
  HiOutlineHome,
  HiOutlineUserGroup,
  HiOutlinePhone,
  HiOutlineXCircle,
  HiArrowLeftOnRectangle,
} from 'react-icons/hi2';

import UnstyledLink from '@/shared/components/links/UnstyledLink';
import logger from '@/shared/lib/logger';
import IconButton from '@/shared/components/buttons/IconButton';
import Login from '@/modules/auth/login';
import dynamic from 'next/dynamic';
import Skeleton from '../Skeleton';

const links = [
  { href: '/', label: 'Κεντρική', Icon: HiOutlineHome },
  { href: '/about-us', label: 'Σχετικά με εμάς', Icon: HiOutlineUserGroup },
  { href: '/contact', label: 'Επικοινωνία', Icon: HiOutlinePhone },
];

export default function Sidebar() {
  const [openSidebar, setOpenSidebar] = React.useState(false);
  return (
    <>
      <span className='absolute top-5 right-4 z-10 cursor-pointer text-2xl text-white'>
        <IconButton
          variant='dark'
          icon={HiBars3BottomLeft}
          isDarkBg
          onClick={() => setOpenSidebar((state) => !state)}
        />
      </span>
      {openSidebar && (
        <div
          className='sidebar fixed right-0 top-0 bottom-0 z-10
    h-screen w-[300px] overflow-y-auto bg-gray-900 p-3 text-center text-white text-dark shadow duration-1000'
        >
          <div className='text-xl '>
            <div className='flex flex-row items-center justify-between rounded-md '>
              <div className='flex flex-row'>
                <MdOutlineAgriculture className='mr-2 text-3xl text-gray-200' />
                <h1 className='text-[15px] text-xl font-bold text-gray-200'>
                  Farm-Employee
                </h1>
              </div>

              <HiOutlineXCircle
                onClick={() => setOpenSidebar(false)}
                className='cursor-pointer text-2xl text-red-400'
              />
            </div>
            <hr className='my-2 text-gray-600' />

            <div>
              {links.map(({ href, label, Icon }) => (
                <div
                  key={`${href}${label}`}
                  className='mt-2 flex cursor-pointer items-center rounded-md p-2 px-4 duration-300  hover:bg-indigo-600'
                >
                  <Icon className='text-gray-200' />
                  <span className='ml-4 text-[15px] text-gray-200'>
                    {label}
                  </span>
                </div>
              ))}

              <hr className='my-2 text-gray-600' />
              <Login />
              {/* <div className='mt-3 flex cursor-pointer items-center rounded-md p-2 px-4 duration-300  hover:bg-indigo-600'>
                <HiArrowLeftOnRectangle className='mr-2 text-3xl text-gray-200' />
                <span className='ml-4 text-[15px] text-gray-200'>Logout</span>
                
              </div> */}
            </div>
          </div>
        </div>
      )}
    </>
  );
}
