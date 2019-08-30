const validate = values => {
  const errors = {};

  var nameReg = new RegExp("^[a-zA-Z][a-zA-Z_]{0,14}[a-zA-Z]$");

  if (values.name) {
    var res = nameReg.test(values.name);
    if (!res) {
      errors.name = "Invalid format. Must between 16 characters and greater than characters 2";
    }
  } else {
    errors.name = "DoxaTag is required";
  }

  return errors;
};

export default validate;
