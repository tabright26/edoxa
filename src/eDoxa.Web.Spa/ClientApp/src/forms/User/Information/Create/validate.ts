export const validate = values => {
  const nameRegExp = new RegExp("^[A-Z](((-)[A-Z])|[a-z]){1,15}$");
  const yearRegExp = new RegExp("^[0-9]{4}$");
  const monthRegExp = new RegExp("^(0[1-9]|1[012])$");
  const dayRegExp = new RegExp("^(0[1-9]|[12]\\d|3[01])$");
  const errors: any = {};
  if (!values.firstName) {
    errors.firstName = "First name is required";
  } else if (!nameRegExp.test(values.firstName)) {
    errors.firstName = "Invalid first name";
  }
  if (!values.lastName) {
    errors.lastName = "Last name is required";
  } else if (!nameRegExp.test(values.lastName)) {
    errors.lastName = "Invalid last name";
  }
  if (!values.year) {
    errors.year = "Year of birth is required";
  } else if (!yearRegExp.test(values.year)) {
    errors.year = "Invalid year";
  }
  if (!values.month) {
    errors.month = "Month of birth is required";
  } else if (!monthRegExp.test(values.month)) {
    errors.month = "Invalid month";
  }
  if (!values.day) {
    errors.day = "Day of birth is required";
  } else if (!dayRegExp.test(values.day)) {
    errors.day = "Invalid day";
  }
  if (!values.gender) {
    errors.gender = "Gender is required";
  }
  return errors;
};
