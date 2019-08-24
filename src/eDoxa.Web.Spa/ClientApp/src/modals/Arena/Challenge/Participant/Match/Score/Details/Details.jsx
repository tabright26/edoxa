import React from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalFooter, ModalHeader, Table } from "reactstrap";
import Button from "../../../../../../../components/Button";
import Format from "../../../../../../../components/Format";
import { ARENA_CHALLENGE_PARTICIPANT_MATCH_SCORE_DETAILS_MODAL } from "../../../../../../../modals";

const ArenaChallengeParticipantMatchScoreDetailsModal = ({ show, handleHide, stats }) => (
  <Modal isOpen={show} toggle={handleHide} className="modal-primary">
    <ModalHeader toggle={handleHide}>Score Details</ModalHeader>
    <Table className="mb-0" size="sm" responsive striped dark>
      <thead>
        <tr>
          <th>Name</th>
          <th className="text-center">Value</th>
          <th className="text-center" />
          <th className="text-center">Weighting</th>
          <th className="text-center" />
          <th className="text-center">Score</th>
        </tr>
      </thead>
      <tbody>
        {stats.map((stat, index) => (
          <tr key={index}>
            <td>{stat.name}</td>
            <td className="text-center">{stat.value}</td>
            <td className="text-center">&#10005;</td>
            <td className="text-center">{stat.weighting}</td>
            <td className="text-center">&#61;</td>
            <td className="text-center text-primary">
              <Format.Score score={stat.score} />
            </td>
          </tr>
        ))}
        <tr>
          <th colSpan={5}>Score</th>
          <th className="text-center text-primary">
            <Format.Score score={stats.reduce((totalScore, stat) => totalScore + stat.score, 0)} />
          </th>
        </tr>
      </tbody>
    </Table>
    <ModalFooter>
      <Button.Close onClick={handleHide} />
    </ModalFooter>
  </Modal>
);

export default connectModal({ name: ARENA_CHALLENGE_PARTICIPANT_MATCH_SCORE_DETAILS_MODAL })(ArenaChallengeParticipantMatchScoreDetailsModal);
