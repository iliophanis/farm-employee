import * as React from 'react';
import { RiAlarmWarningFill } from 'react-icons/ri';

import Layout from '@/shared/components/layout/Layout';
import ArrowLink from '@/shared/components/links/ArrowLink';
import Seo from '@/shared/components/Seo';
import Button from '@/shared/components/buttons/Button';

export default function ContactPage() {
  return (
    <Layout>
      <Seo templateTitle='Contact' />

      <main>
        <section className='bg-white dark:bg-gray-900'>
          <div className='mx-auto max-w-screen-md py-8 px-4 lg:py-16'>
            <h2 className='mb-4 text-center text-4xl font-extrabold tracking-tight text-gray-900 dark:text-white'>
              Επικοινωνία
            </h2>
            <p className='mb-8 text-center font-light text-gray-500 dark:text-gray-400 sm:text-xl lg:mb-16'>
              Έχετε κάποιο τεχνικό πρόβλημα; Θέλετε να στείλετε σχόλια σχετικά
              με μια λειτουργία beta; Ενημέρωσέ μας.
            </p>
            <form action='#' className='space-y-8'>
              <div>
                <label
                  htmlFor='email'
                  className='mb-2 block text-sm font-medium text-gray-900 dark:text-gray-300'
                >
                  Email
                </label>
                <input
                  type='email'
                  id='email'
                  className='dark:shadow-sm-light block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 shadow-sm focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500'
                  placeholder='farmemployee3@gmail.com'
                  required
                />
              </div>
              <div>
                <label
                  htmlFor='subject'
                  className='mb-2 block text-sm font-medium text-gray-900 dark:text-gray-300'
                >
                  Θέμα
                </label>
                <input
                  type='text'
                  id='subject'
                  className='dark:shadow-sm-light block w-full rounded-lg border border-gray-300 bg-gray-50 p-3 text-sm text-gray-900 shadow-sm focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500'
                  placeholder='Πως μπορούμε να σας βοηθήσουμε'
                  required
                />
              </div>
              <div className='sm:col-span-2'>
                <label
                  htmlFor='message'
                  className='mb-2 block text-sm font-medium text-gray-900 dark:text-gray-400'
                >
                  Μήνυμα
                </label>
                <textarea
                  id='message'
                  rows={6}
                  className='block w-full rounded-lg border border-gray-300 bg-gray-50 p-2.5 text-sm text-gray-900 shadow-sm focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500'
                  placeholder='Αφήστε ένα σχόλιο...'
                ></textarea>
              </div>
              <div className='align-center flex flex-row justify-center'>
                <Button variant='primary' type='submit'>
                  Αποστολή
                </Button>
              </div>
            </form>
          </div>
        </section>
      </main>
    </Layout>
  );
}
