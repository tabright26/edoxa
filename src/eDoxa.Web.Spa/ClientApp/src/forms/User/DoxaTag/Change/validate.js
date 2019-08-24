const validate = values => {
  const errors = {};
  if (!values.name) {
    errors.name = "DoxaTag required.";
  } else if (values.name.length < 2 && values.name.length > 16) {
    errors.name = "Must between 16 characters and greater than characters 2.";
  } else if (!/^[0-9a-zA-Z_]*$/.test(values.name)) {
    errors.name = "Invalid format.";
  }
  return errors;
};

export default validate;
