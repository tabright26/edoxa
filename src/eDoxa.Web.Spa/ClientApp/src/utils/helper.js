export const months = (function getMonths() {
  const months = [];
  for (let month = 1; month <= 12; month++) {
    months.push(month);
  }
  return months;
})();

export const days = (function getDays() {
  const days = [];
  for (let day = 1; day <= 31; day++) {
    days.push(day);
  }
  return days;
})();

export const years = (function getYears() {
  const date = new Date(Date.now());
  const years = [];
  for (let year = date.getFullYear() - 75; year <= date.getFullYear(); year++) {
    years.push(year);
  }
  return years;
})();
