export const validate = values => {
  var nameRegExp = new RegExp("^[a-zA-Z][a-zA-Z_]{0,14}[a-zA-Z]$");
  const errors: any = {};
  if (!values.name) {
    errors.name = "Doxatag is required";
  } else if (!nameRegExp.test(values.name)) {
    errors.name =
      "Invalid format. Must between 16 characters and greater than characters 2";
  }
  return errors;
};
