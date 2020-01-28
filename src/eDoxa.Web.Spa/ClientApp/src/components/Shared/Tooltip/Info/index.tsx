import React, { FunctionComponent, useState } from "react";
import { Popover, PopoverHeader, PopoverBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faQuestionCircle } from "@fortawesome/free-solid-svg-icons";

interface Props {
  id: string;
}

export const Info: FunctionComponent<Props> = ({ id, children }) => {
  const [open, setOpen] = useState(false);
  return (
    <>
      <span id={id}>
        <FontAwesomeIcon className="text-muted" icon={faQuestionCircle} />
      </span>
      <Popover
        style={{
          width: "250px"
        }}
        placement="right"
        isOpen={open}
        target={id}
        trigger="hover"
        delay={{ show: 0, hide: 250 }}
        toggle={() => setOpen(!open)}
      >
        <PopoverHeader className="text-uppercase">
          <FontAwesomeIcon className="text-muted" icon={faQuestionCircle} />{" "}
          Information
        </PopoverHeader>
        <PopoverBody>{children}</PopoverBody>
      </Popover>
    </>
  );
};
