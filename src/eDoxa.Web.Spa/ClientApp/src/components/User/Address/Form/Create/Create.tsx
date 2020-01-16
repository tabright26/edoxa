import React, { FunctionComponent } from "react";
import { FormGroup, Col, Form } from "reactstrap";
import {
  Field,
  reduxForm,
  InjectedFormProps,
  FormErrors,
  formValueSelector
} from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import FormField from "components/User/Address/Field";
import { CREATE_USER_ADDRESS_FORM } from "utils/form/constants";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { createUserAddress } from "store/actions/identity";
import {
  throwSubmissionError,
  getFieldValidationRuleMessage
} from "utils/form/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import InputMask from "react-input-mask";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import {
  AddressFieldsOptions,
  AddressValidatorOptions,
  CountryRegionOptions
} from "types";

interface OwnProps {}

interface StateProps {
  fieldsOptions: AddressFieldsOptions;
  validatorOptions: AddressValidatorOptions;
  regions: CountryRegionOptions[];
}

interface FormData {
  country: string;
  line1: string;
  line2: string;
  city: string;
  state: string;
  postalCode: string;
}

type OutterProps = OwnProps & {
  handleCancel: () => void;
};

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel,
  reset,
  regions,
  fieldsOptions: { country, line1, line2, city, state, postalCode }
}) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <FormField.Country
      label={country.label}
      placeholder={country.placeholder}
      onChange={() => reset()}
    />
    <Field
      type="text"
      name="line1"
      label={line1.label}
      placeholder={line1.placeholder}
      formGroup={FormGroup}
      component={Input.Text}
    />
    {!line2.excluded && (
      <Field
        type="text"
        name="line2"
        label={line2.label}
        placeholder={line2.placeholder}
        formGroup={FormGroup}
        component={Input.Text}
      />
    )}
    <Field
      type="text"
      name="city"
      label={city.label}
      placeholder={city.placeholder}
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup row className="my-0">
      <Col xs="8">
        {!state.excluded && (
          <FormField.State
            label={state.label}
            placeholder={state.placeholder}
            regions={regions}
          />
        )}
      </Col>
      <Col xs="4">
        {!postalCode.excluded && (
          <Field
            type="text"
            name="postalCode"
            label={postalCode.label}
            placeholder={postalCode.placeholder}
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

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  const selector = formValueSelector(CREATE_USER_ADDRESS_FORM);
  const countryTwoIso = selector(state, "country");
  const {
    default: { address },
    addressBook: { countries }
  } = state.static.identity.data;
  const countryOptions = countries.find(
    country => country.twoIso === countryTwoIso
  );
  return {
    initialValues: {
      country: "CA", // TODO: Retrieved default country from user claims.
      state: "AB"
    },
    fieldsOptions: address.fields,
    validatorOptions: address.validator,
    regions: countryOptions ? countryOptions.regions : []
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: CREATE_USER_ADDRESS_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(createUserAddress(values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel(),
    validate: (values, { fieldsOptions, validatorOptions }) => {
      const errors: FormErrors<FormData> = {};
      for (let [key, value] of Object.entries(validatorOptions)) {
        errors[key] = getFieldValidationRuleMessage(value, values[key]);
      }

      // errors.line1 = tryGetFieldValidationRuleMessage(
      //   validatorOptions.line1,
      //   values.line1
      // );
      // if (fieldsOptions.line2.excluded) {
      //   errors.line2 = tryGetFieldValidationRuleMessage(
      //     validatorOptions.line2,
      //     values.line2
      //   );
      // }
      // errors.city = tryGetFieldValidationRuleMessage(
      //   validatorOptions.city,
      //   values.city
      // );
      // if (fieldsOptions.state.excluded) {
      //   errors.state = tryGetFieldValidationRuleMessage(
      //     validatorOptions.state,
      //     values.state
      //   );
      // }
      // if (fieldsOptions.postalCode.excluded) {
      //   errors.postalCode = tryGetFieldValidationRuleMessage(
      //     validatorOptions.postalCode,
      //     values.postalCode
      //   );
      // }
      return errors;
    }
  })
);

export default enhance(CustomForm);
