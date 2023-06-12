import React, { Dispatch, SetStateAction, useEffect, useState } from 'react';
import {
  IEmployeeRequestItem,
  IEmployeeRequestResponse,
} from '@/modules/employee/requests/models/IEmployeeRequestList';
import Skeleton from '@/shared/components/Skeleton';

import { HiDocumentText, HiOutlineInformationCircle } from 'react-icons/hi2';
import InfiniteScroll from 'react-infinite-scroll-component';
import Button from '@/shared/components/buttons/Button';

type IRequestsListProps = {
  isPersonalRequests: boolean;
  type: string;
  data: IEmployeeRequestItem[];
  setData: Dispatch<SetStateAction<IEmployeeRequestItem[]>>;
  loading: boolean;
  userRequests: IEmployeeRequestResponse;
  handleChangePage: () => void;
  handleDelRequest: (empReqId: number) => void;
  handleOpenDetails: (id: number) => void;
};
const RequestsList = ({
  isPersonalRequests,
  type,
  data,
  setData,
  loading,
  userRequests,
  handleChangePage,
  handleDelRequest,
  handleOpenDetails,
}: IRequestsListProps) => {
  useEffect(() => {
    console.log({ data });
    setData([]);
  }, []);

  useEffect(() => {
    console.log({ data });
    if (!loading) setData((state) => state.concat(userRequests.data));
  }, [userRequests, loading]);

  if (loading && data.length === 0) return <Skeleton className='w-100 h-100' />;

  if (!loading && data.length === 0)
    return (
      <div
        id='alert-1'
        className='mb-4 flex rounded-lg bg-blue-50 p-4 text-blue-800 dark:bg-gray-800 dark:text-blue-400'
        role='alert'
      >
        <HiOutlineInformationCircle className='h-5 w-5 flex-shrink-0' />
        <span className='sr-only'>Info</span>
        <div className='ml-3 text-sm font-medium'>
          Δεν υπάρχουν διαθέσιμες αιτήσεις.
        </div>
      </div>
    );

  return (
    <InfiniteScroll
      dataLength={data.length}
      next={handleChangePage}
      hasMore={loading ? false : data.length < userRequests.totalSize}
      loader={<Skeleton />}
      className='mb-7 grid grid-cols-1 gap-0 pl-10 pr-10'
    >
      <>
        {data.map((d: IEmployeeRequestItem, idx) => (
          <div className='border-l-2 border-purple-600' key={idx}>
            <div className=''>
              <div className='-ml-3.5 flex h-7 w-7 items-center justify-center rounded-full bg-purple-600'>
                <HiDocumentText className='h-5 w-5 text-white' />
              </div>
              <div className='rounded-lg bg-gray-100 p-4 shadow-lg'>
                <div className='mb-3 flex justify-between'>
                  <a
                    href='#!'
                    className='text-sm font-medium text-purple-600 transition duration-300 ease-in-out hover:text-purple-700 focus:text-purple-800'
                  >
                    {d.jobType}
                  </a>
                  <a
                    href='#!'
                    className='text-sm font-medium text-purple-600 transition duration-300 ease-in-out hover:text-purple-700 focus:text-purple-800'
                  >
                    Ημ. 'Εναρξης : {d.startJobDate}
                  </a>
                </div>

                <div className='mb-2 text-gray-700'>
                  <div className='flex flex-row items-center text-gray-900'>
                    Αγρότης : {d.farmer.name} ({d.farmer.email}) {' - '}
                    {d.farmer.contactInfo}
                  </div>
                  <div className='flex flex-row items-center text-gray-900'>
                    Καλλιέργεια : {d.cultivationName}
                  </div>

                  <div className='flex flex-row items-center text-gray-900'>
                    Τοποθεσία : {d.location.displayName}
                  </div>

                  <div className='flex flex-row items-center text-gray-900'>
                    Ποσό Πληρωμής : {d.price !== null ? `${d.price}€` : '-'}
                  </div>
                </div>
                <div className='flex flex-row justify-end'>
                  <Button
                    variant='primary'
                    className='mr-2'
                    size='sm'
                    onClick={() => handleOpenDetails(d.requestId)}
                  >
                    Προβολή
                  </Button>
                  {isPersonalRequests && (
                    <Button
                      variant='red'
                      size='sm'
                      onClick={() => handleDelRequest(d.employeeRequestId)}
                    >
                      Διαγραφή
                    </Button>
                  )}
                </div>
              </div>
            </div>
          </div>
        ))}
        <div className='align-center mt-2 mb-5 flex flex-row justify-center '>
          {data.length < userRequests.totalSize && (
            <Button
              variant='outline'
              onClick={handleChangePage}
              isLoading={loading}
              className='flex w-1/6 flex-row justify-center'
            >
              ({data.length}/{userRequests.totalSize}) Περισσότερα...
            </Button>
          )}
        </div>
      </>
    </InfiniteScroll>
  );
};

export default RequestsList;
