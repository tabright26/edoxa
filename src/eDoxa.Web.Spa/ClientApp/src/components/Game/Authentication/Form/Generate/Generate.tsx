import React, { FunctionComponent } from "react";
import { Form, FormGroup, Label } from "reactstrap";
import { Field, reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { GENERATE_GAME_AUTHENTICATION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { Game } from "types";
import { generateGameAuthentication } from "store/actions/game";
import { throwSubmissionError } from "utils/form/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface FormData {}

interface OutterProps {
  game: Game;
  setAuthenticationFactor: (data: any) => any;
}

type InnerProps = InjectedFormProps<FormData, Props> & {
  stripe: stripe.Stripe;
};

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({ handleSubmit, error }) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <FormGroup>
      <Label>Summoner name</Label>
      <Field name="summonerName" component={Input.Text} />
    </FormGroup>
    <FormGroup>
      <Label>Region</Label>
      <Field name="region" component={Input.Select} disabled={true}>
        <option value="NA">North America</option>
      </Field>
    </FormGroup>
    <Button.Submit className="w-25">Search</Button.Submit>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: GENERATE_GAME_AUTHENTICATION_FORM,
    onSubmit: async (values, dispatch: any, { game }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(generateGameAuthentication(game, values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch, { setAuthenticationFactor }) => {
      setAuthenticationFactor(result.data);
    }
  })
);

export default enhance(CustomForm);
