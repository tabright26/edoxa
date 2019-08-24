const generateMonths = () => {
  const months = [];
  for (let month = 1; month <= 12; month++) {
    months.push(month);
  }
  return months;
};

const generateDays = () => {
  const days = [];
  for (let day = 1; day <= 31; day++) {
    days.push(day);
  }
  return days;
};

const generateYears = () => {
  const date = new Date(Date.now());
  const years = [];
  for (let year = date.getFullYear() - 75; year <= date.getFullYear(); year++) {
    years.push(year);
  }
  return years;
};

export const months = generateMonths();
export const days = generateDays();
export const years = generateYears();
