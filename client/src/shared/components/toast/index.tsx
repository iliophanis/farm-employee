import React from 'react';
import clsxm from '@/shared/lib/clsxm';
import toast from 'react-hot-toast';
import { MdOutlineClose } from 'react-icons/md';
import {
  HiCheckCircle,
  HiExclamationCircle,
  HiExclamationTriangle,
  HiXCircle,
} from 'react-icons/hi2';
import { IconType } from 'react-icons';

export const notify = (
  title: string,
  description: string,
  icon: React.ReactElement
) =>
  toast.custom(
    (t) => (
      <div
        className={clsxm(
          'relative z-50 flex w-96 translate-y-0 transform-gpu flex-row items-center justify-between rounded-xl bg-gray-900 px-4 py-6 text-white shadow-2xl transition-all duration-500 ease-in-out hover:translate-y-1 hover:shadow-none',
          t.visible ? 'top-0' : '-top-96'
        )}
      >
        <div className='text-xl'>{icon}</div>
        <div className='ml-4 flex cursor-default flex-col items-start justify-center'>
          <h1 className='text-base font-semibold leading-none tracking-wider text-white'>
            {title}
          </h1>
          <p className='mt-2 text-sm leading-relaxed tracking-wider text-gray-200'>
            {description}
          </p>
        </div>
        <div
          className='absolute top-2 right-2 cursor-pointer text-lg'
          onClick={() => toast.dismiss(t.id)}
        >
          <MdOutlineClose />
        </div>
      </div>
    ),
    { id: 'unique-notification', position: 'top-center' }
  );

export const errorNotify = (title: string, description: string) =>
  notify(title, description, <HiXCircle className='text-red-500' />);

export const successNotify = (title: string, description: string) =>
  notify(title, description, <HiCheckCircle className='text-green-500' />);

export const warningNotify = (title: string, description: string) =>
  notify(
    title,
    description,
    <HiExclamationTriangle className='text-yellow-500' />
  );

export const infoNotify = (title: string, description: string) =>
  notify(title, description, <HiExclamationCircle className='text-blue-500' />);
