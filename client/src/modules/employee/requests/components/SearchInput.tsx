import Button from '@/shared/components/buttons/Button';
import IconButton from '@/shared/components/buttons/IconButton';
import useEventListener from '@/shared/hooks/useEventListener';
import React, { useEffect, useRef } from 'react';
import { HiOutlineSearch, HiXCircle } from 'react-icons/hi';

type SearchInputProps = {
  type: string;
  filter: string;
  handleChangeFilter: (value: string, clear?: boolean) => void;
};

const SearchInput = ({
  filter,
  handleChangeFilter,
  type,
}: SearchInputProps) => {
  const searchRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    if (searchRef.current !== null) {
      searchRef.current.value = '';
    }
  }, [type]);
  useEventListener('keydown', (evt: any) => {
    if (evt.keyCode !== 13) return;
    handleChangeFilter(searchRef.current?.value ?? '');
  });

  return (
    <>
      <label
        htmlFor='default-search'
        className='sr-only mb-2 text-sm font-medium text-gray-900 dark:text-white'
      >
        Search
      </label>
      <div className='relative'>
        <div className='pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3'>
          <HiOutlineSearch className='h-5 w-5 text-gray-500 dark:text-gray-400' />
        </div>
        <input
          type='search'
          id='default-search'
          className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-3 pl-10 text-xs text-gray-900 focus:border-indigo-500 focus:ring-indigo-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-indigo-500 dark:focus:ring-indigo-500'
          placeholder='Αναζητήστε Email Αγρότη, Καλλιέργεια, Πόλη, Είδος Εργασίας'
          ref={searchRef}
        />
        {filter !== '' && (
          <IconButton
            variant='ghost'
            icon={HiXCircle}
            onClick={() => {
              handleChangeFilter('', true);
              searchRef.current!.value! = '';
            }}
            className='absolute right-28 bottom-1 rounded-lg text-red-500'
          />
        )}
        <Button
          variant='primary'
          size='sm'
          onClick={() => handleChangeFilter(searchRef.current?.value ?? '')}
          className='absolute right-2.5 bottom-1 rounded-lg  px-4 py-1.5'
        >
          Αναζήτηση
        </Button>
      </div>
    </>
  );
};

export default SearchInput;
