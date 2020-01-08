import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { UNLINK_GAME_CREDENTIAL_FORM } from "forms";
import { compose } from "recompose";
import { Game } from "types";
import { unlinkGameCredential, loadGames } from "store/actions/game";
import {
  UnlinkGameCredentialAction,
  UNLINK_GAME_CREDENTIAL_SUCCESS,
  UNLINK_GAME_CREDENTIAL_FAIL
} from "store/actions/game/types";
import { throwSubmissionError } from "utils/form/types";
import authorize from "utils/oidc/AuthorizeService";

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
      return await dispatch(unlinkGameCredential(game)).then(
        (action: UnlinkGameCredentialAction) => {
          switch (action.type) {
            case UNLINK_GAME_CREDENTIAL_SUCCESS: {
              return dispatch(loadGames()).then(() => {
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
            case UNLINK_GAME_CREDENTIAL_FAIL: {
              throwSubmissionError(action.error);
              break;
            }
          }
          return Promise.resolve(action);
        }
      );
    }
  })
);

export default enhance(CustomForm);
