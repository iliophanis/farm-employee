import * as React from 'react';
import { RiAlarmWarningFill } from 'react-icons/ri';

import Layout from '@/shared/components/layout/Layout';
import ArrowLink from '@/shared/components/links/ArrowLink';
import Seo from '@/shared/components/Seo';

export default function AboutUsPage() {
  return (
    <Layout>
      <Seo templateTitle='AboutUs' />

      <main>
        <section className='font-poppins flex items-center bg-white dark:bg-white xl:h-screen '>
          <div className='mx-auto max-w-6xl flex-1 justify-center py-4 md:px-6 lg:py-6'>
            <div className='flex flex-wrap '>
              <div className='mb-10 w-full px-4 lg:mb-0 lg:w-1/2'>
                <div className='relative lg:max-w-md'>
                  <img
                    src='/favicon/full_logo.png'
                    alt='aboutimage'
                    className='relative z-10 h-full w-full rounded object-cover'
                  />
                  <div className='absolute bottom-0 right-0 z-10 rounded border-4 border-blue-500 bg-white p-8 shadow dark:border-blue-400 dark:bg-gray-800 dark:text-gray-300 sm:p-8 lg:-mb-8 lg:-mr-11 '>
                    <p className='text-lg font-semibold md:w-72'>
                      <svg
                        xmlns='http://www.w3.org/2000/svg'
                        fill='currentColor'
                        className='absolute top-0 left-0 h-16 w-16 text-blue-700 opacity-10 dark:text-gray-300'
                        viewBox='0 0 16 16'
                      >
                        <path d='M12 12a1 1 0 0 0 1-1V8.558a1 1 0 0 0-1-1h-1.388c0-.351.021-.703.062-1.054.062-.372.166-.703.31-.992.145-.29.331-.517.559-.683.227-.186.516-.279.868-.279V3c-.579 0-1.085.124-1.52.372a3.322 3.322 0 0 0-1.085.992 4.92 4.92 0 0 0-.62 1.458A7.712 7.712 0 0 0 9 7.558V11a1 1 0 0 0 1 1h2Zm-6 0a1 1 0 0 0 1-1V8.558a1 1 0 0 0-1-1H4.612c0-.351.021-.703.062-1.054.062-.372.166-.703.31-.992.145-.29.331-.517.559-.683.227-.186.516-.279.868-.279V3c-.579 0-1.085.124-1.52.372a3.322 3.322 0 0 0-1.085.992 4.92 4.92 0 0 0-.62 1.458A7.712 7.712 0 0 0 3 7.558V11a1 1 0 0 0 1 1h2Z'></path>
                      </svg>{' '}
                      Με ένα κλικ, το Farm Employee φέρνει την εργασία στα
                      αγροκτήματα πιο κοντά σε εσένα.
                    </p>
                  </div>
                </div>
              </div>
              <div className='mb-10 w-full px-6 lg:mb-0 lg:w-1/2 '>
                <div className='mb-6 border-l-4 border-blue-500 pl-4 '>
                  <span className='text-sm  text-gray-600 dark:text-gray-400'>
                    Ποιοί είμαστε?
                  </span>
                  <h1 className='mt-2 text-3xl font-black text-gray-700 dark:text-gray-300 md:text-5xl'>
                    Σχετικά με εμάς
                  </h1>
                </div>
                <p className='mb-6 text-base leading-7 text-gray-700 dark:text-gray-400'>
                  Είμαστε μια πλήρως καταρτισμένη εταιρία με ένα σαφές όραμα: να
                  επιλύσουμε το πρόβλημα της εργασίας στα αγροκτήματα και να
                  καθιστούμε την αγροτική εργασία τόσο απλή όσο ένα κλικ.
                  <br />
                  <br />
                  To farm employee φέρνει σε επικοινωνία τους αγρότες, τους
                  αμπελουργούς, τους κτηνοτρόφους και οποιονδήποτε χρειάζεται
                  εργατικά χέρια, σε ελεύθερη αναζήτηση και επικοινωνία με τους
                  ανθρώπους (αγρότες, μαθητές, φοιτητές, οδηγοί, κ.λπ.) που
                  θέλουν και είναι διαθέσιμοι να εργαστούν, κοντά τον τόπο
                  μόνιμης διαμονής τους, αλλά και σε άλλες περιοχές της χώρας,
                  προκειμένου να απασχολούνται όσο το δυνατόν περισσότερες
                  ημέρες, αφού είναι γνωστή η εποχικότητα των αγροεργασιών.
                  <br />
                  <br />
                  Έρχεται και καλύπτει ένα σημαντικό κενό στη διαδικασία
                  αναζήτησης και εύρεσης εργατών και σε μια εποχή όπου
                  παρατηρείται έλλειψη εργατικών χεριών στον αγροτικό χώρο.
                  <br />
                  <br />Η χρήση του δεν απαιτεί ιδιαίτερες ψηφιακές ικανότητες
                  τόσο από την πλευρά των εργατών, όσο και από την πλευρά των
                  αγροτών και κτηνοτρόφων, γεγονός που το καθιστά φιλικό και
                  ελκυστικό.
                </p>
              </div>
            </div>
          </div>
        </section>
      </main>
    </Layout>
  );
}
