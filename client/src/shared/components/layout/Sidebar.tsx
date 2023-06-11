import * as React from 'react';
import { MdOutlineAgriculture } from 'react-icons/md';
import {
  HiBars3BottomLeft,
  HiOutlineHome,
  HiOutlineUserGroup,
  HiOutlinePhone,
  HiOutlineXCircle,
  HiOutlineDocumentText,
} from 'react-icons/hi2';
import { useRouter } from 'next/router';
import IconButton from '@/shared/components/buttons/IconButton';
import Login from '@/modules/auth/login';
import { useAuth } from '@/shared/contexts/AuthProvider';

const unAuthorizedMenu = [
  { href: '/', label: 'Κεντρική', Icon: HiOutlineHome },
  { href: '/about-us', label: 'Σχετικά με εμάς', Icon: HiOutlineUserGroup },
  { href: '/contact', label: 'Επικοινωνία', Icon: HiOutlinePhone },
];

const employeeMenu = [
  { href: '/', label: 'Κεντρική', Icon: HiOutlineHome },
  {
    href: '/employee/requests',
    label: 'Αιτήσεις',
    Icon: HiOutlineDocumentText,
  },
  { href: '/about-us', label: 'Σχετικά με εμάς', Icon: HiOutlineUserGroup },
  { href: '/contact', label: 'Επικοινωνία', Icon: HiOutlinePhone },
];

const farmerMenu = [
  { href: '/', label: 'Κεντρική', Icon: HiOutlineHome },
  { href: '/farmer/requests', label: 'Αιτήσεις', Icon: HiOutlineDocumentText },
  { href: '/about-us', label: 'Σχετικά με εμάς', Icon: HiOutlineUserGroup },
  { href: '/contact', label: 'Επικοινωνία', Icon: HiOutlinePhone },
]; //button Create Request

export default function Sidebar() {
  const router = useRouter();
  const [openSidebar, setOpenSidebar] = React.useState(false);
  const { auth, isAuthenticated } = useAuth();
  const menuOptions = React.useMemo(
    () =>
      isAuthenticated()
        ? auth?.user.isEmployee
          ? employeeMenu
          : farmerMenu
        : unAuthorizedMenu,
    [auth, isAuthenticated]
  );
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
              {menuOptions.map(({ href, label, Icon }) => (
                <div
                  key={`${href}${label}`}
                  className='mt-2 flex cursor-pointer items-center rounded-md p-2 px-4 duration-300  hover:bg-indigo-600'
                  onClick={() => {
                    setOpenSidebar(false);
                    router.push(href);
                  }}
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
