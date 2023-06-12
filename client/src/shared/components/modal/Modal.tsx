import { FunctionComponent } from 'react';

import Button from '../buttons/Button';

export type ModalButtonProps = {
  onClick: () => void;
  isDisabled: boolean;
  isLoading: boolean;
  label: string;
  variant?: 'primary' | 'outline' | 'ghost' | 'light' | 'dark' | 'red';
};

type IProps = {
  openModal: boolean;
  setOpenModal: any;
  icon: any;
  buttons?: ModalButtonProps[];
  children: any;
};

const Modal: FunctionComponent<IProps> = ({
  openModal,
  setOpenModal,
  icon,
  buttons,
  children,
}) => {
  return openModal ? (
    <div className='fixed inset-0 z-10 overflow-y-auto'>
      <div className='flex min-h-screen items-center justify-center'>
        <div className='fixed inset-0 bg-gray-500 bg-opacity-40 transition-opacity'></div>
        <div className='inline-block transform overflow-hidden rounded-lg bg-gray-700 text-left align-bottom shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-lg sm:align-middle'>
          <div
            className='overflow-y-auto bg-gray-700 px-4 pt-5 pb-4 sm:p-6 sm:pb-4'
            style={{ maxHeight: '80vh' }}
          >
            <div className='mx-auto flex h-12 w-12 flex-shrink-0 items-center justify-center rounded-full  border-2 border-indigo-600 bg-white text-dark'>
              {icon}
            </div>
            {children}
          </div>
          <div className='bg-gray-50 px-4 py-3'>
            {buttons && buttons.length > 0 && (
              <div className='align-center mb-2 flex flex-row justify-between'>
                {buttons.map((btn, idx) => (
                  <Button
                    key={idx}
                    variant={btn.variant ? btn.variant : 'primary'}
                    onClick={btn.onClick}
                    isLoading={btn.isLoading}
                    disabled={btn.isDisabled || btn.isLoading}
                    className={`w-full justify-center rounded-md ${
                      idx === 0 ? '' : 'ml-2'
                    }`}
                  >
                    {btn.label}
                  </Button>
                ))}
              </div>
            )}
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
