import { useMemo } from 'react';
import styles from '@/shared/components/formControls/formControls.module.css';

type SelectProps = {
  name: string;
  label: string;
  icon?: string;
  register: any;
  errorName?: any;
  disabled?: boolean;
  options: any;
  optionKey?: string;
  optionValue?: string;
  multiple?: boolean;
  placeholder: string;
};

const UncontrolledSelect = ({
  name,
  label,
  icon,
  register,
  errorName,
  disabled = false,
  options,
  optionKey = 'value',
  optionValue = 'label',
  placeholder,
}: SelectProps) => {
  const items = useMemo(
    () =>
      options.map((item: any, index: number) => (
        <option key={index} value={item[optionKey]}>
          {item[optionValue]}
        </option>
      )),
    [options, optionKey, optionValue]
  );

  return (
    <>
      <div>
        <div className='col-span-6 sm:col-span-3'>
          <label
            htmlFor={name}
            className='block text-sm font-medium text-white'
          >
            {label}
          </label>
          <div className='relative mt-1 rounded-md shadow-sm'>
            <select
              id={name}
              name={name}
              disabled={disabled}
              {...register}
              className={`mt-1 block w-full rounded-md text-gray-900 ${
                errorName ? 'border-2 border-red-500' : 'border border-gray-300'
              } bg-white py-2 px-3 shadow-sm focus:border-indigo-500 focus:outline-none focus:ring-indigo-500 sm:text-sm`}
            >
              <option value=''>{placeholder}</option>
              {items}
            </select>
          </div>
        </div>
        {errorName && (
          <div className='pl-16px text-red-500'>
            <small>{errorName.message}</small>
          </div>
        )}
      </div>
    </>
  );
};

export default UncontrolledSelect;
