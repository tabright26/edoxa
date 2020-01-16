import React, { FunctionComponent } from "react";
import { FormGroup, Col, Form } from "reactstrap";
import {
  Field,
  reduxForm,
  InjectedFormProps,
  FormErrors,
  formValueSelector
} from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import FormField from "components/User/Address/Field";
import { UPDATE_USER_ADDRESS_FORM } from "utils/form/constants";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { updateUserAddress } from "store/actions/identity";
import {
  throwSubmissionError,
  getFieldValidationRuleMessage
} from "utils/form/types";
import { connect, MapStateToProps } from "react-redux";
import {
  AddressId,
  AddressFieldsOptions,
  AddressValidatorOptions,
  CountryRegionOptions
} from "types";
import { RootState } from "store/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import InputMask from "react-input-mask";

interface StateProps {
  fieldsOptions: AddressFieldsOptions;
  validatorOptions: AddressValidatorOptions;
  regions: CountryRegionOptions[];
}

interface FormData {
  line1: string;
  line2: string;
  city: string;
  state: string;
  postalCode: string;
}

interface OutterProps {
  addressId: AddressId;
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel,
  regions,
  reset,
  fieldsOptions: { country, line1, line2, city, state, postalCode }
}) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <FormField.Country placeholder={country.label} disabled />
    <Field
      type="text"
      name="line1"
      placeholder={line1.label}
      formGroup={FormGroup}
      component={Input.Text}
    />
    {!line2.excluded && (
      <Field
        type="text"
        name="line2"
        placeholder={line2.label}
        formGroup={FormGroup}
        component={Input.Text}
      />
    )}
    <Field
      type="text"
      name="city"
      placeholder={city.label}
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup row className="my-0">
      <Col xs="8">
        {!state.excluded && (
          <FormField.State placeholder={state.label} regions={regions} />
        )}
      </Col>
      <Col xs="4">
        {!postalCode.excluded && (
          <Field
            type="text"
            name="postalCode"
            placeholder={postalCode.label}
            formGroup={FormGroup}
            component={Input.Text}
            tag={InputMask}
            mask={postalCode.mask}
            maskChar={null}
            formatChars={{
              "1": "[0-9]",
              A: "[A-Z]"
            }}
          />
        )}
      </Col>
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel
        onClick={() => {
          handleCancel();
          reset();
        }}
      />
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<StateProps, Props, RootState> = (
  state,
  ownProps
) => {
  const selector = formValueSelector(UPDATE_USER_ADDRESS_FORM);
  const countryTwoIso = selector(state, "country");
  const { data } = state.root.user.addressBook;
  const {
    default: { address },
    addressBook: { countries }
  } = state.static.identity.data;
  const countryOptions = countries.find(
    country => country.twoIso === countryTwoIso
  );
  return {
    initialValues: data.find(address => address.id === ownProps.addressId),
    fieldsOptions: address.fields,
    validatorOptions: address.validator,
    regions: countryOptions ? countryOptions.regions : []
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: UPDATE_USER_ADDRESS_FORM,
    onSubmit: async (values, dispatch, { addressId }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(updateUserAddress(addressId, values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel(),
    validate: (values, { fieldsOptions, validatorOptions }) => {
      const errors: FormErrors<FormData> = {};
      for (let [key, value] of Object.entries(validatorOptions)) {
        if (fieldsOptions[key].excluded) {
          errors[key] = getFieldValidationRuleMessage(value, values[key]);
        }
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
