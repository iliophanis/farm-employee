import React, { Dispatch, useEffect, useMemo, useState } from 'react';
import { MdOutlineAgriculture } from 'react-icons/md';

import Modal, { ModalButtonProps } from '@/shared/components/modal/Modal';
import { UserRequest } from '@/modules/home/models/IUserRequest';
import DescriptionList from '@/shared/components/list/DescriptionList';
import logger from '@/shared/lib/logger';
import { useAuth } from '@/shared/contexts/AuthProvider';
import clsxm from '@/shared/lib/clsxm';
import useRequestDetailsModal from '@/modules/home/components/requestDetailsModal/useRequestDetailsModal';
import FarmerDetailsModal from '@/modules/home/components/farmerDetailsModal';
import {
  ISubEmployee,
  ISubmitRequest,
} from '@/modules/home/models/ISubmitRequest';
import {
  UnControlledInput as TextInput,
  UncontrolledSelect as SelectInput,
} from '@/shared/components/formControls';
import { useForm } from 'react-hook-form';
import Button from '@/shared/components/buttons/Button';
import {
  HiInformationCircle,
  HiMinusCircle,
  HiOutlineChevronDown,
  HiOutlineChevronUp,
  HiOutlineInformationCircle,
  HiPlus,
  HiTrash,
  HiXCircle,
} from 'react-icons/hi2';
import { HiUserAdd, HiUserRemove } from 'react-icons/hi';
import { useArray } from '@/shared/hooks/useArray';
import IconButton from '@/shared/components/buttons/IconButton';
type IRequestDetailsModalProps = {
  openModal: boolean;
  setOpenModal: Dispatch<boolean>;
  data: UserRequest;
};

const RequestDetailsModal = ({
  openModal,
  setOpenModal,
  data,
}: IRequestDetailsModalProps) => {
  const { auth } = useAuth();
  const { items: subEmployees, add, remove, clear } = useArray([]);
  const { onSubmitRequest, loading } = useRequestDetailsModal();
  const [openFarmerModal, setOpenFarmerModal] = useState(false);
  const [showSubEmployeeForm, setShowSubEmployeeForm] = useState(false);
  const [showInfoAlert, setShowInfoAlert] = useState(true);
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<ISubEmployee>();
  useEffect(() => {
    reset();
    clear();
    setShowSubEmployeeForm(false);
    setShowInfoAlert(true);
  }, [auth, data]);
  const modalButtons: ModalButtonProps[] = useMemo(() => {
    if (auth?.user.isEmployee) {
      const isDisabled = !data.actions.includes('Submit');
      const payload: ISubmitRequest = {
        requestId: data.id,
        subEmployees: [],
      };
      const teamPayload = {
        requestId: data.id,
        subEmployees: subEmployees,
      };
      return [
        {
          label: 'Ατομική Αίτηση',
          onClick: () => onSubmitRequest(payload, setOpenModal),
          isDisabled: isDisabled,
          isLoading: loading, //TODO setDynamically
        },
        {
          label: 'Ομαδική Αίτηση',
          onClick: () => onSubmitRequest(teamPayload, setOpenModal),
          isDisabled: isDisabled || subEmployees.length === 0,
          isLoading: loading, //TODO setDynamically
        },
      ];
    }
    return [];
  }, [auth, data, onSubmitRequest, loading, setOpenModal]);

  const items = [
    {
      caption: 'Τύπος Εργασίας',
      value: data.jobType,
    },
    {
      caption: 'Καλλιέργεια',
      value: data.cultivationName,
    },
    {
      caption: 'Τοποθεσία',
      value: data.location.displayName,
    },
    {
      caption: 'Αγρότης',
      value: (
        <div
          className={clsxm(
            'animated-underline custom-link cursor-pointer ',
            'text-white hover:text-primary-200 active:text-primary-700',
            'border-b border-dotted border-primary-300 hover:border-black/0'
          )}
          onClick={() => setOpenFarmerModal(true)}
        >
          {data.farmer.name} ({data.farmer.email})
        </div>
      ),
    },
    {
      caption: 'Ημ. Έναρξης Εργασίας',
      value: data.startJobDate,
    },
    {
      caption: 'Εκτιμώμενη Διάρκεια Εργασίας',
      value:
        `${data.estimatedDuration} ${
          data.estimatedDuration == 1 ? 'ημέρα' : 'ημέρες'
        }` || '-',
    },
    {
      caption: 'Ποσό Πληρωμής',
      value: `${data.price}€` || '-',
    },
  ];
  const isSubmitEnabled = useMemo(
    () => auth?.user.isEmployee && data.actions.includes('Submit'),
    [data, auth]
  );
  return (
    <>
      <Modal
        openModal={openModal}
        setOpenModal={setOpenModal}
        buttons={modalButtons}
        icon={<MdOutlineAgriculture />}
      >
        <DescriptionList items={items} />
        {isSubmitEnabled && subEmployees.length === 0 && showInfoAlert && (
          <div
            id='alert-1'
            className='mb-4 flex rounded-lg bg-blue-50 p-4 text-blue-800 dark:bg-gray-800 dark:text-blue-400'
            role='alert'
          >
            <HiOutlineInformationCircle className='h-5 w-5 flex-shrink-0' />
            <span className='sr-only'>Info</span>
            <div className='ml-3 text-sm font-medium'>
              Αν θέλετε να αιτηθείτε ομαδικά την εργασία, πατήστε το κουμπί
              "Υπάλληλος". Έπειτα, θα ενεργοποιηθεί η Ομαδική Αίτηση.
            </div>
            <IconButton
              variant='ghost'
              icon={HiXCircle}
              onClick={() => setShowInfoAlert(false)}
              className='-mx-1.5 -my-1.5 ml-auto inline-flex h-8 w-8 rounded-lg bg-blue-50 p-1.5 text-blue-500 hover:bg-blue-200 focus:ring-2 focus:ring-blue-400 dark:bg-gray-800 dark:text-blue-400 dark:hover:bg-gray-700'
            ></IconButton>
          </div>
        )}
        {isSubmitEnabled && subEmployees && subEmployees.length > 0 && (
          <>
            <div className='p-3'>
              <div className='flex flex-row items-center text-white'>
                Υπάλληλοι
              </div>
              <div className='text-gray-300'>
                <ul role='list' className='mt-2 divide-y divide-gray-100'>
                  {subEmployees.map((s: ISubEmployee, idx: number) => (
                    <li className='flex justify-between gap-x-6' key={idx}>
                      <div className='flex gap-x-4' key={idx}>
                        <div className='min-w-0 flex-auto'>
                          <p className='text-base font-bold leading-6 text-gray-100'>
                            {++idx}. {s.firstName} {s.lastName} ({s.email})
                          </p>
                        </div>
                      </div>
                      <div className='flex flex-col items-end'>
                        <IconButton
                          icon={HiTrash}
                          onClick={() => remove(s)}
                          variant='ghost'
                          className='text-red-500'
                        />
                      </div>
                    </li>
                  ))}
                </ul>
              </div>
            </div>
          </>
        )}
        {isSubmitEnabled && (
          <div className='align-center flex flex-row justify-center'>
            <Button
              variant='outline'
              leftIcon={
                showSubEmployeeForm ? HiOutlineChevronUp : HiOutlineChevronDown
              }
              onClick={() => setShowSubEmployeeForm((state) => !state)}
            >
              Υπάλληλος
            </Button>
          </div>
        )}

        {isSubmitEnabled && showSubEmployeeForm && (
          <>
            <div className='border-white/1 mb-1 border-b'>
              <h2 className='text-base font-semibold leading-7 text-gray-100'>
                Στοιχεία Υπαλλήλου
              </h2>
            </div>
            <div className='grid grid-cols-2 gap-4'>
              <TextInput
                label='Όνομα'
                type='text'
                register={register('firstName', {
                  required: 'To όνομα είναι υποχρεωτικό',
                })}
                errorName={errors.firstName}
              />
              <TextInput
                label='Επώνυμο'
                type='text'
                register={register('lastName', {
                  required: 'To επώνυμο είναι υποχρεωτικό',
                })}
                errorName={errors.lastName}
              />
            </div>
            <div className='mb-2'>
              <TextInput
                label='Email'
                type='text'
                register={register('email', {
                  required: 'To email είναι υποχρεωτικό',
                })}
                errorName={errors.email}
              />
            </div>
            <div className='mb-2'>
              <TextInput
                label='Διεύθυνση'
                type='text'
                register={register('contactInfo.address')}
                errorName={errors.contactInfo?.address}
              />
            </div>
            <div className='grid grid-cols-2 gap-2'>
              <TextInput
                label='ΤΚ'
                type='number'
                register={register('contactInfo.tk')}
                errorName={errors.contactInfo?.tk}
              />
              <TextInput
                label='Πόλη'
                type='text'
                register={register('contactInfo.city')}
                errorName={errors.contactInfo?.city}
              />
              <TextInput
                label='Σταθερό Τηλ.'
                type='number'
                register={register('contactInfo.phoneNo')}
                errorName={errors.contactInfo?.phoneNo}
              />
              <TextInput
                label='Κινητό Τηλ.'
                type='number'
                register={register('contactInfo.mobilePhoneNo', {
                  required: 'Η κινητό τηλέφωνο είναι υποχρεωτικό',
                })}
                errorName={errors.contactInfo?.mobilePhoneNo}
              />
            </div>
            <div className='align-center mt-1 flex flex-row justify-center'>
              <Button
                variant='primary'
                leftIcon={HiUserAdd}
                onClick={handleSubmit((data) => {
                  add(data);
                  reset();
                  setShowSubEmployeeForm(false);
                })}
              >
                Προσθήκη
              </Button>
            </div>
          </>
        )}
      </Modal>
      <FarmerDetailsModal
        openModal={openFarmerModal}
        setOpenModal={setOpenFarmerModal}
        data={data.farmer}
      />
    </>
  );
};

export default RequestDetailsModal;
