import React from "react";
import { Table, Button, Modal, ModalHeader, ModalFooter } from "reactstrap";

import Format from "../../../../../../../Shared/Formats";

const ArenaChallengeParticipantMatchStatListModal = ({ className, isOpen, toggle, stats }) => (
  <Modal isOpen={isOpen} toggle={toggle} className={"modal-primary " + className}>
    <ModalHeader toggle={toggle}>Score Details</ModalHeader>
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
      <Button color="primary" onClick={toggle}>
        Close
      </Button>
    </ModalFooter>
  </Modal>
);

export default ArenaChallengeParticipantMatchStatListModal;
