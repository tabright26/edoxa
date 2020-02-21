import React, { useState, FunctionComponent } from "react";
import GameAuthenticationFrom from "components/Service/Game/Authentication/Form";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";
import { GameOptions, Game } from "types/games";
import { MapStateToProps, connect } from "react-redux";
import { RootState } from "store/types";
import { compose } from "recompose";
import { WorkflowProps } from "views/Workflow";

type OwnProps = {
  game: Game;
};

type StateProps = { gameOptions: GameOptions };

type InnerProps = StateProps;

type OutterProps = OwnProps &
  WorkflowProps & {
    show: boolean;
    handleHide: () => void;
  };

type Props = InnerProps & OutterProps;

const Workflow: FunctionComponent<Props> = ({
  show,
  handleHide,
  gameOptions,
  nextWorkflowStep
}) => {
  const [authenticationFactor, setAuthenticationFactor] = useState(null);
  return !authenticationFactor ? (
    <>
      <p className="text-primary">
        <strong>
          SMURF ACCOUNTS ARE NOT ALLOWED ON EDOXA.GG, IF CAUGHT, IT'S AN INSTANT
          BAN WITH NO REFUNDS!
        </strong>
      </p>
      {show && (
        <GameAuthenticationFrom.Generate
          game={gameOptions.name}
          setAuthenticationFactor={setAuthenticationFactor}
          nextWorkflowStep={nextWorkflowStep}
        />
      )}
    </>
  ) : (
    <>
      <div className="d-flex justify-content-between">
        <div className="text-center">
          <h5>Current</h5>
          <img
            src={authenticationFactor.currentSummonerProfileIconBase64}
            alt="current"
            height={100}
            width={100}
          />
        </div>
        <div className="my-auto w-50 px-3 text-center">
          <div className="text-muted">
            {gameOptions.services.find(x => x.name === "Game").instructions}
          </div>
          <FontAwesomeIcon className="mt-2" icon={faArrowRight} size="3x" />
        </div>
        <div className="text-center">
          <h5>Expected</h5>
          <img
            src={authenticationFactor.expectedSummonerProfileIconBase64}
            alt="expected"
            height={100}
            width={100}
          />
        </div>
      </div>
      <div className="d-flex justify-content-center mt-3">
        {show && (
          <GameAuthenticationFrom.Validate
            gameOptions={gameOptions}
            handleCancel={handleHide}
            setAuthenticationFactor={setAuthenticationFactor}
            nextWorkflowStep={nextWorkflowStep}
          />
        )}
      </div>
    </>
  );
};

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  return {
    gameOptions: state.static.games.games.find(
      x => x.name.toUpperCase() === ownProps.game.toUpperCase()
    )
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(Workflow);
