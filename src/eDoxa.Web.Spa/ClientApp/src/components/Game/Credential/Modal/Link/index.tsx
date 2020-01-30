import React, { useState, FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { LINK_GAME_CREDENTIAL_MODAL } from "utils/modal/constants";
import GameAuthenticationFrom from "components/Game/Authentication/Form";
import { compose } from "recompose";
import { GameOptions } from "types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";

type InnerProps = InjectedProps & { gameOptions: GameOptions };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Link: FunctionComponent<Props> = ({ show, handleHide, gameOptions }) => {
  const [authenticationFactor, setAuthenticationFactor] = useState(null);
  return (
    <Modal
      unmountOnClose={false}
      backdrop="static"
      centered
      isOpen={show}
      toggle={handleHide}
    >
      <ModalHeader className="text-uppercase my-auto bg-gray-900" toggle={handleHide}>
        <strong>{gameOptions.displayName} Authentications</strong>
      </ModalHeader>
      <ModalBody>
        {!authenticationFactor ? (
          <GameAuthenticationFrom.Generate
            game={gameOptions.name}
            setAuthenticationFactor={setAuthenticationFactor}
          />
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
                  {
                    gameOptions.services.find(x => x.name === "Game")
                      .instructions
                  }
                </div>
                <FontAwesomeIcon
                  className="mt-2"
                  icon={faArrowRight}
                  size="3x"
                />
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
              <GameAuthenticationFrom.Validate
                gameOptions={gameOptions}
                handleCancel={handleHide}
                setAuthenticationFactor={setAuthenticationFactor}
              />
            </div>
          </>
        )}
      </ModalBody>
    </Modal>
  );
};

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: LINK_GAME_CREDENTIAL_MODAL, destroyOnHide: false })
);

export default enhance(Link);
