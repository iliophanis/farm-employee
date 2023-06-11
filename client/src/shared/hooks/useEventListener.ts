import { useEffect, useRef } from 'react';

type EventType = keyof WindowEventMap;

const useEventListener = <T extends HTMLElement>(
  eventType: EventType,
  callback: (event: Event) => void,
  element: T | Window = window
) => {
  const callbackRef = useRef<(event: Event) => void>(callback);

  useEffect(() => {
    callbackRef.current = callback;
  }, [callback]);

  useEffect(() => {
    const handler = (evt: Event) => callbackRef.current(evt);
    element.addEventListener(eventType, handler);
    return () => element.removeEventListener(eventType, handler);
  }, [eventType, element]);
};

export default useEventListener;
