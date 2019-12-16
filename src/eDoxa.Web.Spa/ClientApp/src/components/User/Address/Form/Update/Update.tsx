import React, { FunctionComponent } from "react";
import { FormGroup, Col, Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps } from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import FormField from "components/Shared/Form/Field";
import { UPDATE_USER_ADDRESS_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { updateUserAddress } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import {
  COUNTRY_REQUIRED,
  countryRegex,
  COUNTRY_INVALID,
  LINE1_REQUIRED,
  line1Regex,
  LINE1_INVALID,
  line2Regex,
  LINE2_INVALID,
  CITY_REQUIRED,
  cityRegex,
  CITY_INVALID,
  stateRegex,
  STATE_INVALID,
  postalRegex,
  POSTAL_INVALID
} from "validation";
import { connect, MapStateToProps } from "react-redux";
import { AddressId } from "types";
import { RootState } from "store/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface Props {
  addressId: AddressId;
}

interface FormData {
  line1: string;
  line2: string;
  city: string;
  state: string;
  postalCode: string;
}

interface StateProps {}

const validate = values => {
  const errors: any = {};
  if (!values.country) {
    errors.country = COUNTRY_REQUIRED;
  } else if (!countryRegex.test(values.country)) {
    errors.country = COUNTRY_INVALID;
  }
  if (!values.line1) {
    errors.line1 = LINE1_REQUIRED;
  } else if (!line1Regex.test(values.line1)) {
    errors.line1 = LINE1_INVALID;
  }
  if (values.line2 && !line2Regex.test(values.line2)) {
    errors.line2 = LINE2_INVALID;
  }
  if (!values.city) {
    errors.city = CITY_REQUIRED;
  } else if (!cityRegex.test(values.city)) {
    errors.city = CITY_INVALID;
  }
  if (values.state && !stateRegex.test(values.state)) {
    errors.state = STATE_INVALID;
  }
  if (values.postalCode && !postalRegex.test(values.postalCode)) {
    errors.postalCode = POSTAL_INVALID;
  }
  return errors;
};

async function submit(values, dispatch, addressId) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: AxiosActionCreatorMeta = { resolve, reject };
      dispatch(updateUserAddress(addressId, values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const UpdateUserAddressForm: FunctionComponent<InjectedFormProps<FormData> &
  Props &
  any> = ({ dispatch, handleSubmit, handleCancel, addressId, error }) => (
  <Form
    onSubmit={handleSubmit(data =>
      submit(data, dispatch, addressId).then(() => handleCancel())
    )}
  >
    {error && <FormValidation error={error} />}
    <FormGroup>
      <FormField.Country disabled={true} />
    </FormGroup>
    <Field
      type="text"
      name="line1"
      label="Address line 1"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="line2"
      label="Address line 2 (optional)"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="city"
      label="City"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup row className="my-0">
      <Col xs="8">
        <Field
          type="text"
          name="state"
          label="State"
          formGroup={FormGroup}
          component={Input.Text}
        />
      </Col>
      <Col xs="4">
        <Field
          type="text"
          name="postalCode"
          label="Postal Code"
          formGroup={FormGroup}
          component={Input.Text}
        />
      </Col>
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<StateProps, Props, RootState> = (
  state: RootState,
  ownProps: Props
) => {
  const { data } = state.root.user.addressBook;
  return {
    initialValues: data.find(address => address.id === ownProps.addressId)
  };
};

const enhance = compose<any, any>(
  reduxForm<FormData, Props>({
    form: UPDATE_USER_ADDRESS_FORM,
    validate
  }),
  connect(mapStateToProps)
);

export default enhance(UpdateUserAddressForm);
