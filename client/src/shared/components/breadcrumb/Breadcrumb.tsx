import clsxm from '@/shared/lib/clsxm';
import { useRouter } from 'next/router';
import React from 'react';
import { HiOutlineChevronRight } from 'react-icons/hi2';
type BreadcrumbProps = {
  data: { Icon?: any; label: string; href?: string }[];
};

const Breadcrumb = ({ data }: BreadcrumbProps) => {
  const router = useRouter();
  return (
    <nav
      className='flex rounded-lg border border-gray-200 bg-gray-50 px-5 py-2 text-gray-700 dark:border-gray-700 dark:bg-gray-800'
      aria-label='Breadcrumb'
    >
      <ol className='inline-flex items-center space-x-1 md:space-x-2'>
        {data.map((d, idx) => {
          const isLastItem = idx === data.length - 1;
          return (
            <li className='inline-flex items-center' key={idx}>
              <div
                onClick={isLastItem ? () => {} : () => router.push(d.href!)}
                className={clsxm(
                  'inline-flex items-center text-sm font-medium text-gray-700 ',
                  isLastItem
                    ? ''
                    : 'cursor-pointer hover:text-blue-600 dark:text-gray-400 dark:hover:text-white'
                )}
              >
                {idx === 0 ? (
                  <d.Icon className='mr-2 h-5 w-5' />
                ) : (
                  <HiOutlineChevronRight className='h-5 w-5 text-gray-400' />
                )}
                {idx === 0 ? (
                  d.label
                ) : (
                  <span className='ml-1 text-sm font-medium text-gray-500 dark:text-gray-400 md:ml-2'>
                    {d.label}
                  </span>
                )}
              </div>
            </li>
          );
        })}
      </ol>
    </nav>
  );
};

export default Breadcrumb;
