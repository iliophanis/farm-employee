import { FunctionComponent } from 'react';

type IProps = {
  openModal: boolean;
  setOpenModal: any;
  icon: any;
  buttonName: string;
  buttonColor?: string;
  onClick: any;
  children: JSX.Element;
};

const Modal: FunctionComponent<IProps> = ({
  openModal,
  setOpenModal,
  icon,
  buttonName,
  buttonColor = 'indigo',
  onClick,
  children,
}) => {
  const bgColor =
    buttonColor === 'indigo'
      ? 'bg-indigo-600'
      : buttonColor === 'red'
      ? 'bg-red-600'
      : 'bg-blue-600';
  const hoverBgColor =
    buttonColor === 'indigo'
      ? 'bg-indigo-700'
      : buttonColor === 'red'
      ? 'bg-red-700'
      : 'bg-blue-700';
  const focusBgColor =
    buttonColor === 'indigo'
      ? 'ring-indigo-500'
      : buttonColor === 'red'
      ? 'ring-red-400'
      : 'ring-blue-500';
  return openModal ? (
    <div className='fixed inset-0 z-10 overflow-y-auto'>
      <div className='flex min-h-screen items-center justify-center'>
        <div className='fixed inset-0 bg-gray-500 bg-opacity-40 transition-opacity'></div>
        <div className='inline-block transform overflow-hidden rounded-lg bg-white text-left align-bottom shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-lg sm:align-middle'>
          <div className='bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4'>
            <div className='sm:flex sm:items-start'>
              <div className='mx-auto flex h-12 w-12 flex-shrink-0 items-center justify-center rounded-full bg-red-100 sm:mx-0 sm:h-10 sm:w-10'>
                {icon}
              </div>
              {children}
            </div>
          </div>
          <div className='bg-gray-50 px-4 py-3 sm:flex sm:flex-row-reverse sm:px-6'>
            <button
              onClick={onClick}
              type='button'
              className={`inline-flex w-full justify-center rounded-md border border-transparent px-4 py-2 shadow-sm ${bgColor} text-base font-medium text-white hover:${hoverBgColor} focus:outline-none focus:ring-2 focus:ring-offset-2 focus:${focusBgColor} sm:ml-3 sm:w-auto sm:text-sm`}
            >
              {buttonName}
            </button>
            <button
              type='button'
              onClick={() => setOpenModal(false)}
              className='mt-3 inline-flex w-full justify-center rounded-md border border-gray-300 bg-white px-4 py-2 text-base font-medium text-gray-700 shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 sm:mt-0 sm:ml-3 sm:w-auto sm:text-sm'
            >
              Ακύρωση
            </button>
          </div>
        </div>
      </div>
    </div>
  ) : (
    <div />
  );
};

export default Modal;
