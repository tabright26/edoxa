import React, { FunctionComponent } from "react";
import { Card } from "reactstrap";
import ChallengeForm from "components/Challenge/Form";
import {
  HocUserProfileUserIdStateProps,
  withUserProfileUserId,
  withUserProfileGameIsAuthenticated,
  HocUserProfileGameIsAuthenticatedStateProps
} from "utils/oidc/containers";
import { compose } from "recompose";
import {
  ChallengeId,
  CHALLENGE_STATE_INSCRIPTION,
  Game,
  GameOptions
} from "types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import {
  MapStateToProps,
  connect,
  DispatchProp
} from "react-redux";
import { RootState } from "store/types";
import Button from "components/Shared/Button";
import { show } from "redux-modal";
import { LINK_GAME_CREDENTIAL_MODAL } from "utils/modal/constants";

type Params = {
  readonly challengeId?: ChallengeId;
};

type OwnProps = RouteComponentProps<Params> &
  Params &
  HocUserProfileUserIdStateProps;

type StateProps = {
  readonly canRegister: boolean;
  readonly game: Game;
  readonly gameOptions: GameOptions;
};

type DispatchProps = {
  showLinkGameAccountCredentialModal: () => void;
};

type InnerProps = DispatchProp &
  DispatchProps &
  OwnProps &
  StateProps &
  HocUserProfileGameIsAuthenticatedStateProps;

type OutterProps = Params & {
  readonly className?: string;
};

type Props = InnerProps & OutterProps;

const Register: FunctionComponent<Props> = ({
  className,
  userId,
  isAuthenticated,
  canRegister,
  dispatch,
  gameOptions
}) =>
  canRegister &&
  (isAuthenticated ? (
    <Card className={className}>
      <ChallengeForm.Register userId={userId} />
    </Card>
  ) : (
    <Button.Submit
      type="button"
      color="primary"
      size="lg"
      className="text-uppercase w-100"
      onClick={() =>
        dispatch(
          show(LINK_GAME_CREDENTIAL_MODAL, {
            gameOptions
          })
        )
      }
    >
      Link
    </Button.Submit>
  ));

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.challenge;
  const challenge = data.find(
    challenge =>
      challenge.id ===
      (ownProps.match
        ? ownProps.match.params.challengeId
        : ownProps.challengeId)
  );
  return {
    canRegister:
      challenge.state === CHALLENGE_STATE_INSCRIPTION &&
      !challenge.participants.some(
        participant => participant.user.id === ownProps.userId
      ),
    game: challenge.game,
    gameOptions: state.static.games.games.find(x => x.name === challenge.game)
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  withUserProfileUserId,
  connect(mapStateToProps),
  withUserProfileGameIsAuthenticated
);

export default enhance(Register);
