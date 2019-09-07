const validate = values => {
  var nameRegExp = new RegExp("^[a-zA-Z][a-zA-Z_]{0,14}[a-zA-Z]$");
  const errors = {};
  if (!values.name) {
    errors.name = "DoxaTag is required";
  } else if (!nameRegExp.test(values.name)) {
    errors.name = "Invalid format. Must between 16 characters and greater than characters 2";
  }
  return errors;
};

export default validate;
