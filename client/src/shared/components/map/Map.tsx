import { MapContainer, Marker, Popup, TileLayer } from 'react-leaflet';
import { MdOutlineAgriculture } from 'react-icons/md';
import 'leaflet/dist/leaflet.css';
import styles from './map.module.scss';
import L from 'leaflet';

type IProps = { data?: { longtitude: number; latitude: number }[] };
const Map = ({ data }: IProps) => {
  const customMarker = L.icon({
    iconUrl: '~/images/agirculture.png',
    iconSize: [50, 45],
    iconAnchor: [25, 0],
  });
  return (
    <MapContainer className={styles.map} center={[39.0742, 21.8243]} zoom={7}>
      <TileLayer
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        url='https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png'
      />
      {data?.map((d, idx) => (
        <Marker
          position={[d.latitude, d.longtitude]}
          icon={customMarker}
          key={idx}
        >
          <Popup>You have to loggin ...</Popup>
        </Marker>
      ))}
    </MapContainer>
  );
};

export default Map;
