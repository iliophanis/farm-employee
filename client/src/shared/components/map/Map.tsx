import { MapContainer, Marker, Popup, TileLayer } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import styles from './map.module.scss';
const Map = () => {
  return (
    <MapContainer className={styles.map} center={[39.0742, 21.8243]} zoom={7}>
      <TileLayer url='https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png' />
    </MapContainer>
  );
};

export default Map;
