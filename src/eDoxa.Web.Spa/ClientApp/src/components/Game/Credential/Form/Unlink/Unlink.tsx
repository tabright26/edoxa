import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { UNLINK_GAME_CREDENTIAL_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { Game } from "types";
import { unlinkGameCredential, loadGames } from "store/actions/game";
import { throwSubmissionError } from "utils/form/types";
import authorize from "utils/oidc/AuthorizeService";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface FormData {}

interface OutterProps {
  game: Game;
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> & {
  stripe: stripe.Stripe;
};

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  handleCancel
}) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup className="mb-0">
      <Button.Yes type="submit" className="mr-2" />
      <Button.No onClick={() => handleCancel()} />
    </FormGroup>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: UNLINK_GAME_CREDENTIAL_FORM,
    onSubmit: async (_values, dispatch: any, { game }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(unlinkGameCredential(game, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch: any) => {
      dispatch(loadGames()).then(() => {
        console.log(window.location.pathname);
        return authorize
          .getUser()
          .then(user => console.log(user))
          .then(() =>
            authorize
              .signIn({
                returnUrl: window.location.pathname
              })
              .then(() => authorize.getUser().then(x => console.log(x)))
          );
      });
    }
  })
);

export default enhance(CustomForm);
