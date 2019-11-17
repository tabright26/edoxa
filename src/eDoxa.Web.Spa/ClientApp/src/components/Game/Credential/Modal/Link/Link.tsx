import React, { useState } from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { LINK_GAME_CREDENTIAL_MODAL } from "modals";
import GameAuthenticationFrom from "components/Game/Authentication/Form";
import { compose } from "recompose";

// TODO: FRANCIS NEED PROCESS DETAILS.

const LinkGameAuthenticationModal = ({ show, handleHide, gameOption }) => {
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
            {/* <p className="text-justify">
              weqw ewqe qwe qwen iqwneinwq ienijwq nenwq einw ijneiqjw neijwqn
              ienqwi jeqwje niqjw neiwqjne iqjwneinqwie jnqwiej
              nqijwenqijwneijqwn eijqnweij nqijwne
            </p> */}
            <div className="d-flex justify-content-around">
              <div className="text-center">
                <h5>Current</h5>
                <img
                  src={authenticationFactor.currentSummonerProfileIconBase64}
                  alt="current"
                  height={100}
                  width={100}
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
            {/* <p className="text-justify mt-3">
              weqw ewqe qwe qwen iqwneinwq ienijwq nenwq einw ijneiqjw neijwqn
              ienqwi jeqwje niqjw neiwqjne iqjwneinqwie jnqwiej nqijwe nqijwn
              eijqwn eijqnweij nqijwne
            </p> */}
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

const enhance = compose<any, any>(
  connectModal({ name: LINK_GAME_CREDENTIAL_MODAL })
);

export default enhance(LinkGameAuthenticationModal);
