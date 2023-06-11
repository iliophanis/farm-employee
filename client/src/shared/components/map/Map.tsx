import { MapContainer, TileLayer } from 'react-leaflet';
import styles from './map.module.scss';

const Map = ({ children }: any) => {
  return (
    <MapContainer
      className={styles.map}
      center={[37.98381, 23.727539]}
      zoom={7}
    >
      <TileLayer url='https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png' />
      {children}
    </MapContainer>
  );
};

export default Map;
