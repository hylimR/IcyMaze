#!/usr/bin/env python3
"""Fail if any script reaches for an engine API Unity has since removed.

The stub build cannot catch these on its own: the stubs simply do not declare the
removed members, so a hit reads as an ordinary typo rather than as "this was deleted
from the engine in 2019". Comments and string literals are ignored.
"""
import pathlib
import re
import sys

ASSETS = pathlib.Path(__file__).resolve().parent.parent / "Icy Maze" / "Assets"

RULES = [
    (r"Application\.(LoadLevel\w*|loadedLevel\w*|levelCount|CaptureScreenshot)\b",
     "removed in Unity 2019 - use UnityEngine.SceneManagement.SceneManager"),
    (r"\b(GUIText|GUITexture|ParticleEmitter|SetActiveRecursively)\b",
     "removed in Unity 2018/2019"),
    (r"Screen\.lockCursor\b", "removed - use Cursor.lockState"),
    (r"\b(MasterServer|NetworkView)\b", "old UNet networking, removed"),
    (r"\bFindObjects?OfType\b",
     "deprecated in Unity 6 - use FindFirstObjectByType / FindObjectsByType"),
    (r"\.velocity\b", "renamed in Unity 6 - use Rigidbody.linearVelocity"),
    (r"\b(gameObject|transform|this)\.(rigidbody|collider|renderer|audio|animation|camera|light)\b",
     "component shortcut properties were removed in Unity 5 - use GetComponent<T>()"),
]

COMMENT = re.compile(r"//.*$")
STRING = re.compile(r"\"(?:\\.|[^\"\\])*\"")


def main() -> int:
    failures = []
    for path in sorted(ASSETS.rglob("*.cs")):
        for number, raw in enumerate(path.read_text(encoding="utf-8-sig").splitlines(), 1):
            line = STRING.sub('""', COMMENT.sub("", raw))
            for pattern, reason in RULES:
                if re.search(pattern, line):
                    failures.append(f"{path.relative_to(ASSETS.parent.parent)}:{number}: {raw.strip()}\n    -> {reason}")

    if failures:
        print("Removed or deprecated Unity APIs found:\n")
        print("\n".join(failures))
        return 1

    print(f"No removed Unity APIs in {ASSETS}/**/*.cs")
    return 0


if __name__ == "__main__":
    sys.exit(main())
