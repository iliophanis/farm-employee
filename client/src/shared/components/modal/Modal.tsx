import { FunctionComponent } from 'react';
import TextButton from '../buttons/TextButton';
import Button from '../buttons/Button';

type IProps = {
  openModal: boolean;
  setOpenModal: any;
  icon: any;
  buttonName: string;
  buttonColor?: string;
  onClick: any;
  children: JSX.Element;
  loading?: boolean;
};

const Modal: FunctionComponent<IProps> = ({
  openModal,
  setOpenModal,
  icon,
  buttonName,
  buttonColor = 'indigo',
  onClick,
  loading = false,
  children,
}) => {
  return openModal ? (
    <div className='fixed inset-0 z-10 overflow-y-auto'>
      <div className='flex min-h-screen items-center justify-center'>
        <div className='fixed inset-0 bg-gray-500 bg-opacity-40 transition-opacity'></div>
        <div className='inline-block transform overflow-hidden rounded-lg bg-gray-700 text-left align-bottom shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-lg sm:align-middle'>
          <div className='bg-gray-700 px-4 pt-5 pb-4 sm:p-6 sm:pb-4'>
            <div className='mx-auto flex h-12 w-12 flex-shrink-0 items-center justify-center rounded-full  border-2 border-indigo-600 bg-white text-dark'>
              {icon}
            </div>
            {children}
          </div>
          <div className='bg-gray-50 px-4 py-3'>
            <Button
              variant='primary'
              onClick={onClick}
              isLoading={loading}
              className='mb-2 inline-flex w-full justify-center rounded-md'
            >
              {buttonName}
            </Button>
            <Button
              variant='light'
              className='inline-flex w-full justify-center rounded-md '
              onClick={() => setOpenModal(false)}
            >
              Ακύρωση
            </Button>
          </div>
        </div>
      </div>
    </div>
  ) : (
    <div />
  );
};

export default Modal;
