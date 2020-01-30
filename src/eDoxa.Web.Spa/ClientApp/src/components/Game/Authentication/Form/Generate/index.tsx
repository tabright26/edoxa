import React, { FunctionComponent } from "react";
import { Form, FormGroup, Label } from "reactstrap";
import { Field, reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { GENERATE_GAME_AUTHENTICATION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { Game } from "types";
import { generateGameAuthentication } from "store/actions/game";
import { throwSubmissionError } from "utils/form/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { connect } from "react-redux";

interface FormData {}

type InnerProps = InjectedFormProps<FormData, Props> & {
  stripe: stripe.Stripe;
};

type OutterProps = {
  game: Game;
  setAuthenticationFactor: (data: any) => any;
};

type Props = InnerProps & OutterProps;

const Generate: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  submitting,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    <FormGroup>
      <Label>Summoner name</Label>
      <Field name="summonerName" component={Input.Text} />
    </FormGroup>
    <FormGroup>
      <Label>Region</Label>
      <Field name="region" component={Input.Select} disabled>
        <option value="NA">North America</option>
      </Field>
    </FormGroup>
    <Button.Submit loading={submitting} className="w-25">
      Search
    </Button.Submit>
  </Form>
);

const mapStateToProps = () => {
  return {
    initialValues: {
      region: "NA"
    }
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: GENERATE_GAME_AUTHENTICATION_FORM,
    onSubmit: async (values, dispatch, { game }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(generateGameAuthentication(game, values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, _dispatch, { setAuthenticationFactor }) => {
      setAuthenticationFactor(result.data);
    }
  })
);

export default enhance(Generate);
