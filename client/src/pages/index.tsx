import * as React from 'react';
import Seo from '@/shared/components/Seo';
import Home from '@/modules/home';

export default function HomePage() {
  return (
    <>
      <Seo templateTitle='Home' />
      <main>
        <section className='bg-white'>
          <Home />
        </section>
      </main>
    </>
  );
}
