const validate = values => {
  const errors = {};

  var nameReg = new RegExp("^[A-Z](((-)[A-Z])|[a-z]){1,15}$");
  var yearReg = new RegExp("^[0-9]{4}$");
  var monthReg = new RegExp("^(0[1-9]|1[012])$");
  var dayReg = new RegExp("^(0[1-9]|[12]\\d|3[01])$");

  if (values.firstName) {
    var res = nameReg.test(values.firstName);
    if (!res) {
      errors.firstName = "Invalid first name";
    }
  } else {
    errors.firstName = "First name is required";
  }

  if (values.lastName) {
    res = nameReg.test(values.lastName);
    if (!res) {
      errors.lastName = "Invalid last name";
    }
  } else {
    errors.lastName = "Last name is required";
  }

  if (values.year) {
    res = yearReg.test(values.year);
    if (!res) {
      errors.year = "Invalid year";
    }
  } else {
    errors.year = "Year of birth is required";
  }

  if (values.month) {
    res = monthReg.test(values.month);
    if (!res) {
      errors.month = "Invalid month";
    }
  } else {
    errors.month = "Month of birth is required";
  }

  if (values.day) {
    res = dayReg.test(values.day);
    if (!res) {
      errors.day = "Invalid day";
    }
  } else {
    errors.day = "Day of birth is required";
  }

  if (!values.gender) {
    errors.gender = "Gender is required";
  }

  return errors;
};

export default validate;
