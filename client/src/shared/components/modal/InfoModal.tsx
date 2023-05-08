import React from 'react';
import { FaRegCheckCircle } from 'react-icons/fa';

type IProps = {
  openModal: boolean;
  setOpenModal: any;
};

const InfoModal = ({ openModal, setOpenModal }: IProps) => {
  return openModal ? (
    <div className='fixed inset-0 z-10 overflow-y-auto'>
      <div className='flex min-h-screen items-center justify-center'>
        <div className='fixed inset-0 bg-gray-500 bg-opacity-40 transition-opacity'></div>
        <div className='inline-block transform overflow-hidden rounded-lg bg-white text-left align-bottom shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-lg sm:align-middle'>
          <div className='mt-3 text-center'>
            <div className='mx-auto flex h-12 w-12 items-center justify-center rounded-full bg-green-100'>
              <FaRegCheckCircle className='h-6 w-6 text-green-600' />
            </div>
            <h3 className='text-lg font-medium leading-6 text-gray-900'>
              Επιτυχής Ενεργοποίηση!
            </h3>
            <div className='mt-2 px-7 py-3'>
              <p className='text-sm text-gray-500'>
                Ενεργοποιήσατε επιτυχώς το δωρεάν πακέτο γνωριμιάς και έχετε
                διαθέσιμες 5 απαντήσεις και 7 ημέρες απεριόριστης περιήγησης
                στην εφαρμογή.
              </p>
            </div>
            <div className='items-center px-4 py-3'>
              <button
                id='ok-btn'
                onClick={() => setOpenModal(false)}
                className='w-full rounded-md bg-green-500 px-4 py-2 text-base font-medium text-white shadow-sm hover:bg-green-600 focus:outline-none focus:ring-2 focus:ring-green-300'
              >
                OK
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  ) : (
    <div />
  );
};

export default InfoModal;
