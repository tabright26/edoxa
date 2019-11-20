import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { REGISTER_CHALLENGE_PARTICIPANT_FROM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";

interface Props {
  [key: string]: any;
  readonly className?: string;
}

const RegisterChallengeParticipantForm: FunctionComponent<Props> = ({
  registerChallengeParticipant,
  handleSubmit,
  className
}) => (
  <Form
    onSubmit={handleSubmit(() => registerChallengeParticipant())}
    className="h-100"
  >
    <Button.Submit
      color="primary"
      size="lg"
      className={`text-uppercase ${className}`}
    >
      REGISTER
    </Button.Submit>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, any, string>({
    form: REGISTER_CHALLENGE_PARTICIPANT_FROM,
    validate
  })
);

export default enhance(RegisterChallengeParticipantForm);
