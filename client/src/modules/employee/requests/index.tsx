import SearchInput from '@/modules/employee/requests/components/SearchInput';
import RequestsList from '@/modules/employee/requests/components/requestsList/RequestsList';
import { IEmployeeRequestItem } from '@/modules/employee/requests/models/IEmployeeRequestList';
import useEmployeeRequests from '@/modules/employee/requests/useEmployeeRequests';
import Skeleton from '@/shared/components/Skeleton';
import Button from '@/shared/components/buttons/Button';
import { useAuth } from '@/shared/contexts/AuthProvider';
import React, { useEffect, useState } from 'react';
import { HiOutlineSearch } from 'react-icons/hi';
import {
  HiOutlineChevronRight,
  HiOutlineHome,
  HiOutlineInformationCircle,
} from 'react-icons/hi2';

const EmployeeRequests = () => {
  const auth = useAuth();
  const {
    queryParams,
    data,
    setData,
    userRequests,
    loading,
    handleChangePage,
    handleChangeFilter,
    handleChangeType,
  } = useEmployeeRequests();

  if (!auth.isAuthenticated()) return <></>;
  return (
    <>
      <div className='mt-5 inline-block flex min-w-full flex-row flex-wrap items-center justify-between px-6 align-middle'>
        <div className='select-none text-gray-900'>
          <nav
            className='flex rounded-lg border border-gray-200 bg-gray-50 px-5 py-2 text-gray-700 dark:border-gray-700 dark:bg-gray-800'
            aria-label='Breadcrumb'
          >
            <ol className='inline-flex items-center space-x-1 md:space-x-2'>
              <li className='inline-flex items-center'>
                <a
                  href='#'
                  className='inline-flex items-center text-sm font-medium text-gray-700 hover:text-blue-600 dark:text-gray-400 dark:hover:text-white'
                >
                  <HiOutlineHome className='mr-2 h-5 w-5' />
                  Κεντρική
                </a>
              </li>
              <li aria-current='page'>
                <div className='flex items-center'>
                  <HiOutlineChevronRight className='h-5 w-5 text-gray-400' />

                  <span className='ml-1 text-sm font-medium text-gray-500 dark:text-gray-400 md:ml-2'>
                    Αιτήσεις
                  </span>
                </div>
              </li>
            </ol>
          </nav>
        </div>
      </div>
      <div className='mt-3 flex min-w-full flex-row flex-wrap items-center justify-between py-2 px-6  align-middle'>
        <div className='select-none'>
          <div className='inline-flex rounded-md shadow-sm' role='group'>
            <Button
              variant={queryParams.type === 'personal' ? 'primary' : 'light'}
              size='sm'
              // disabled={queryParams.type === 'personal'}
              onClick={() => handleChangeType('personal')}
              className='rounded-l-lg border  border-gray-200'
            >
              Δικές μου
            </Button>
            <Button
              variant={queryParams.type === 'all' ? 'primary' : 'light'}
              size='sm'
              // disabled={queryParams.type === 'all'}
              onClick={() => handleChangeType('all')}
              className='-ml-0.5 border-t border-b border-gray-200'
            >
              {' '}
              Όλες
            </Button>
          </div>
        </div>
        <div className='mt-2 w-full sm:mt-0 md:w-5/12'>
          <SearchInput
            filter={queryParams.filter}
            handleChangeFilter={handleChangeFilter}
          />
        </div>
      </div>
      <div className='mt-2'>
        <RequestsList
          data={data}
          setData={setData}
          loading={loading}
          userRequests={userRequests}
          isPersonalRequests={queryParams.type === 'personal'}
          handleChangePage={handleChangePage}
        />
      </div>
    </>
  );
};

export default EmployeeRequests;
