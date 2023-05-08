import styles from '@/shared/components/formControls/formControls.module.css';

type InputProps = {
  label: string;
  icon?: string;
  register: any;
  type?: string;
  errorName?: any;
  disabled?: boolean;
};

const UnControlledInput = ({
  label,
  type = 'text',
  icon,
  register,
  errorName,
  disabled = false,
}: InputProps) => {
  return (
    <>
      <div>
        <div className='col-span-6 sm:col-span-3'>
          <label className='block text-sm font-medium text-gray-100'>
            {label}
          </label>
          <div className='relative mt-1 rounded-md shadow-sm'>
            <input
              type={type}
              {...register}
              disabled={disabled}
              className={`${
                errorName ? 'border-2 border-red-500' : 'border border-gray-300'
              } w-full rounded-md  px-3  py-2 text-gray-900 placeholder-gray-500 focus:z-10 focus:border-indigo-500 focus:border-indigo-500 focus:outline-none focus:ring-indigo-500 focus:ring-indigo-500 sm:text-sm`}
            />
          </div>
        </div>
      </div>
      {errorName && (
        <div className='pl-16px float-left text-red-500'>
          <small>{errorName.message}</small>
        </div>
      )}
    </>
  );
};

export default UnControlledInput;
