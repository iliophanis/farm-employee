import * as React from 'react';
import 'leaflet/dist/leaflet.css';
import Layout from '@/shared/components/layout/Layout';
import ArrowLink from '@/shared/components/links/ArrowLink';
import ButtonLink from '@/shared/components/links/ButtonLink';
import UnderlineLink from '@/shared/components/links/UnderlineLink';
import UnstyledLink from '@/shared/components/links/UnstyledLink';
import Seo from '@/shared/components/Seo';
import Vercel from '~/svg/Vercel.svg';
import { MapContainer, Marker, Popup, TileLayer } from 'react-leaflet';
import dynamic from 'next/dynamic';
import Skeleton from '@/shared/components/Skeleton';
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
