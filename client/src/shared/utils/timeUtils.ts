export const convertTimeToMinutes = (minutes: number, hours: number) => {
  const convertedHours = Number(hours) * 60;
  return Math.floor(convertedHours) + Number(minutes);
};
