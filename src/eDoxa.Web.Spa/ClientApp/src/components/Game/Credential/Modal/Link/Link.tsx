import React, { useState, FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { LINK_GAME_CREDENTIAL_MODAL } from "utils/modal/constants";
import GameAuthenticationFrom from "components/Game/Authentication/Form";
import { compose } from "recompose";
import { GameOption } from "types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";

type InnerProps = InjectedProps & { gameOption: GameOption };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const CustomModal: FunctionComponent<Props> = ({
  show,
  handleHide,
  gameOption
}) => {
  const [authenticationFactor, setAuthenticationFactor] = useState(null);
  return (
    <Modal className="modal-dialog-centered" isOpen={show} toggle={handleHide}>
      <ModalHeader toggle={handleHide}>
        <strong>{gameOption.displayName} Authentications</strong>
      </ModalHeader>
      <ModalBody>
        {!authenticationFactor ? (
          <GameAuthenticationFrom.Generate
            game={gameOption.name}
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
                <div className="text-muted">{gameOption.instructions}</div>
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
                game={gameOption.name}
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

export default enhance(CustomModal);
