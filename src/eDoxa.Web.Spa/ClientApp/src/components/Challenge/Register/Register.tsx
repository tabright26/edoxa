import React, { FunctionComponent } from "react";
import { Card } from "reactstrap";
import ChallengeForm from "components/Challenge/Form";
import {
  HocUserIsAuthenticatedStateProps,
  withUserIsAuthenticated,
  HocUserProfileUserIdStateProps,
  withUserProfileUserId
} from "utils/oidc/containers";
import { compose } from "recompose";

type InnerProps = HocUserIsAuthenticatedStateProps &
  HocUserProfileUserIdStateProps;

type OutterProps = {
  readonly className?: string;
  readonly canRegister: boolean;
};

type Props = InnerProps & OutterProps;

const ChallengeRegister: FunctionComponent<Props> = ({
  className,
  userId,
  isAuthenticated,
  canRegister
}) =>
  canRegister &&
  isAuthenticated && (
    <Card className={className}>
      <ChallengeForm.Register userId={userId} />
    </Card>
  );
const enhance = compose<InnerProps, OutterProps>(
  withUserIsAuthenticated,
  withUserProfileUserId
);

export default enhance(ChallengeRegister);
