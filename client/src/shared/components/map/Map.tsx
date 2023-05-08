import { MapContainer, Marker, Popup, TileLayer } from 'react-leaflet';
import { MdOutlineAgriculture } from 'react-icons/md';
import 'leaflet/dist/leaflet.css';
import styles from './map.module.scss';
import L from 'leaflet';
import agricultureImg from './agirculture.png';

const Map = () => {
  // const customMarker = L.icon({
  //   iconUrl: ,
  //   iconSize: [50, 45],
  //   iconAnchor: [25, 0],
  // });
  return (
    <MapContainer className={styles.map} center={[39.0742, 21.8243]} zoom={7}>
      <TileLayer
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        url='https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png'
      />
      {/* <Marker position={[39.0742, 21.8243]} icon={customMarker}>
        <Popup>
          A pretty CSS3 popup. <br /> Easily customizable.
        </Popup>
      </Marker> */}
    </MapContainer>
  );
};

export default Map;
