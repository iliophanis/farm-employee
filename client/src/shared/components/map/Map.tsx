import { MapContainer, TileLayer } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import styles from './map.module.scss';

const Map = ({ children }: any) => {
  return (
    <MapContainer className={styles.map} center={[39.0742, 21.8243]} zoom={7}>
      <TileLayer url='https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png' />
      {children}
    </MapContainer>
  );
};

export default Map;
