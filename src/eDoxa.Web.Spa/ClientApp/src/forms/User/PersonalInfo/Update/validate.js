const validate = values => {
  const errors = {};

  var nameReg = new RegExp("^[A-Z](((-)[A-Z])|[a-z]){1,15}$");

  if (values.firstName) {
    var res = nameReg.test(values.firstName);
    if (!res) {
      errors.firstName = "Invalid first name";
    }
  } else {
    errors.firstName = "First name is required";
  }

  return errors;
};

export default validate;
